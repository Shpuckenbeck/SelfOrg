using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SelfOrg.Data;
using SelfOrg.Models;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using SelfOrg.Models.ManageViewModels;

//-------------------------------Контроллер домашней страницы--------------------------
namespace SelfOrg.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context, UserManager<User> manager)
        {
            _context = context;
            _userManager = manager;
        }
        /// <summary>
        /// Главная страница. Выбираются все посты, сортируются по дате создания
        /// от новых к старым и выводятся на страницу. Сюда перенаправляются
        /// запросы /Posts/ и /Posts/Index/
        /// </summary>
        /// <returns></returns>
        // GET: Posts
        //[Route("")]
        //[Route("Posts")]
        //[Route("Posts/Index")]
        public async Task<IActionResult> Index() //Выбор всех постов
        {
            var applicationDbContext = _context.Posts.Include(p => p.Category).Include(p => p.User).OrderByDescending(p => p.PostDate);
            foreach (Post item in applicationDbContext)
            {
                item.rating = Convert.ToSingle(Math.Round(item.rating, 3)); //округляю рейтинг
            }
            return View(await applicationDbContext.ToListAsync());
        }
        
        //-------------------------------------------------------Стандартные методы---------------------------------------------------
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
