using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SelfOrg.Data;
using SelfOrg.Models;

namespace SelfOrg.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Categories/Details/5
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
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Edit/5
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

        [HttpGet]
        public IActionResult set(int id)
        {
            CatCritViewModel model = new CatCritViewModel();
            model.category = _context.Categories.SingleOrDefault(p => p.CategoryId == id);
            model.crits = _context.Criteria;            
            model.prio = new Priority[_context.Criteria.Count()];
            int count = 0;
            foreach (Criterion item in _context.Criteria)
            {
                CatCrit check = _context.CatCrits.SingleOrDefault(p => ((p.CategoryId == id) && (p.CriterionId == item.CriterionId)));
                if (check != null)
                {
                    model.prio[count] = check.prio;
                }
                count++;
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> set (CatCritViewModel model)
        {
            int count = 0;
            foreach (int item in model.critid)
            {
                CatCrit check = _context.CatCrits.SingleOrDefault(p => ((p.CategoryId == model.catid) && (p.CriterionId == item)));  
                if (check != null)
                {
                    check.prio = model.prio[count];
                    _context.CatCrits.Update(check);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    CatCrit catcrit = new CatCrit();
                    catcrit.CategoryId = model.catid;
                    catcrit.CriterionId = item;
                    catcrit.prio = model.prio[count];
                    _context.CatCrits.Add(catcrit);
                    await _context.SaveChangesAsync();
                }
                count++;
            }
            return RedirectToAction("Index");
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
