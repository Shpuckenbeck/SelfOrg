using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SelfOrg.Data;
using SelfOrg.Models;
using Microsoft.AspNetCore.Authorization;
//Категории
namespace SelfOrg.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;    
        }
        /// <summary>
        /// Окно управления категориями, доступно только администратору.
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles ="admin")]
        // GET: Categories
        public async Task<IActionResult> tech() //-------------------------------Окно управления категориями-----------------------------------------------
        {
            return View(await _context.Categories.ToListAsync());
        }
        //-------------------------------------------------------Стандартные методы---------------------------------------------------
        public IActionResult Index()
        {
            var model = _context.Categories.ToList();
                return View(model);
        }

        // GET: Categories/Details/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .SingleOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CatName,CatDescr")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction("tech");
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.SingleOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CatName,CatDescr")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .SingleOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(m => m.CategoryId == id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Настройка категории, доступно только администратору. 
        /// Параметр на входе – id категории. Создаётся CatCritViewModel – модель для настройки категории, 
        /// в которую помещаются данные о категории с соответствующим id, а также обо всех критериях оценки, существующих в системе.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult set(int id) //Настройка важности критериев для каждой категории
        {
            CatCritViewModel model = new CatCritViewModel();
            model.category = _context.Categories.SingleOrDefault(p => p.CategoryId == id); //выбор нужной категории
            model.crits = _context.Criteria;   //выбор всех критериев         
            model.prio = new Priority[_context.Criteria.Count()]; //сейчас не используется из-за проблем с потоками БД
            //int count = 0;
            //foreach (Criterion item in model.crits)
            //{
            //    CatCrit check = _context.CatCrits.SingleOrDefault(p => ((p.CategoryId == id) && (p.CriterionId == item.CriterionId)));
            //    if (check != null)
            //    {
            //        model.prio[count] = check.prio;
            //    }
            //    count++;
            //}
            return View(model);
        }
        /// <summary>
        /// На вход поступает заполненная модель CatCritViewModel model. Для каждого критерия  из model.crits проверяется, 
        /// есть ли в таблице CatCrits запись, соответствующая этому критерию и этой категории. 
        /// Если такая запись есть, то в неё помещается новое значение, после чего таблица обновляется. 
        /// Если же такой записи ещё не было, то создаётся новая. 
        /// Данные об id категории и критерия, а также значении важности берутся из модели, после чего запись добавляется в таблицу.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> set (CatCritViewModel model) //Настройка важности критериев для каждой категории
        {
            int count = 0;
            foreach (int item in model.critid) //для каждого задействованного критерия
            {
                CatCrit check = _context.CatCrits.SingleOrDefault(p => ((p.CategoryId == model.catid) && (p.CriterionId == item)));  //проверяем, задавалась ли важность ранее
                if (check != null) //если да, то есть запись о приоритете данного критерия для данной категории существует
                {
                    check.prio = model.prio[count]; //обновляем эту запись
                    _context.CatCrits.Update(check); //обновляем БД
                    await _context.SaveChangesAsync();
                }
                else //если приоритет настраивается впервые
                {
                    CatCrit catcrit = new CatCrit(); //создаётся новая запись
                    catcrit.CategoryId = model.catid; //в неё помещаются данные из модели
                    catcrit.CriterionId = item;
                    catcrit.prio = model.prio[count];
                    _context.CatCrits.Add(catcrit); //обавляем запись в БД
                    await _context.SaveChangesAsync();
                }
                count++;
            }
            return RedirectToAction("Index");
        }
        //--------------------------Стандартный метод---------------------------------------
        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
