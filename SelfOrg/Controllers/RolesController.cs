using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SelfOrg.Models;
using SelfOrg.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
//–оли и роли пользователей
namespace SelfOrg.Controllers
{
    [Authorize(Roles ="admin")]
    /*«десь всЄ чуть сложнее. ƒоступ к контроллеру ролей есть только у администраторов,
     однако выставить администратора можно только из контроллера ролей. Ќа данный момент
     в развЄрнутом приложении создана роль администратора. ≈сли будет необходимо дать
     права администратора новому пользователю, есть два варианта. ѕервый - проставить
     соответствие в таблице AspNerUserRoles, сделать это можно из панели Azure.
     ¬торой - задеплоить на сервер код с убранным [Authorize...] в этом контроллере,
     зайти на сервере в контроллер, выставить себе админ-привелегии, вернуть ограничение
     в код и передеплоить*/
    
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<User> _userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        /// <summary>
        /// ¬ывод доступных ролей
        /// </summary>
        /// <returns></returns>
        public IActionResult Index() => View(_roleManager.Roles.ToList());

        public IActionResult Create() => View();
        /// <summary>
        /// —оздание роли. Ќа входе - строка с названием. ≈сли строка не пуста, создаЄтс€
        /// нова€ роль через RoleManager с указанным именем, иначе вывод€тс€ ошибки
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// ¬ывод списка пользователей
        /// </summary>
        /// <returns></returns>
        public IActionResult UserList() => View(_userManager.Users.ToList());

        /// <summary>
        /// ”правление рол€ми пользователей. ƒл€ пользовател€, имеющего указанный id, выбираетс€
        /// список доступных ролей, а также имеющихс€ у него. Ёти списки, а также информаци€ о пользователе, передаютс€ в ChangeRoleViewModel
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string userId)
        {
            // получаем пользовател€
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользовател€
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }
        /// <summary>
        /// ќбновление списка ролей пользовател€
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            // получаем пользовател€
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользовател€
                var userRoles = await _userManager.GetRolesAsync(user);
                // получаем все роли
                var allRoles = _roleManager.Roles.ToList();
                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("UserList");
            }

            return NotFound();
        }
    }
}