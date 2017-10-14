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
using System.Security.Claims;
using SelfOrg.Components;

namespace SelfOrg.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Comments.Include(c => c.Post).Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> viewpost(int id)
        {
            CommentViewModel model = new CommentViewModel();
            var post = await _context.Posts.Include(p => p.User).Include(p => p.Category).SingleOrDefaultAsync(p => p.PostID == id);
            model.post = post;
            model.comments = _context.Comments.Where(p => p.PostId == id);
            model.crits =  _context.Criteria;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> viewpost(int id, string comment)
        {
           
            Comment newcom = new Comment();
            ClaimsPrincipal currentUser = this.User;
            newcom.UserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            newcom.PostId = id;
            newcom.Text = comment;
            newcom.CommentDate = DateTime.Now;
            _context.Comments.Add(newcom);
            await _context.SaveChangesAsync();
            CommentViewModel model = new CommentViewModel();
            var post = await _context.Posts.Include(p => p.User).Include(p => p.Category).SingleOrDefaultAsync(p => p.PostID == id);
            model.post = post;
            model.comments = _context.Comments.Where(p => p.PostId == id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> reply([FromBody] ReplyViewModel inmodel) //ответ на комментарий
        {

            Comment newcom = new Comment();
            ClaimsPrincipal currentUser = this.User;
            newcom.UserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            int neededid = Convert.ToInt32(inmodel.CommentId);
            Comment com = await _context.Comments.Where(p => p.CommentId == neededid).SingleOrDefaultAsync();
            newcom.PostId = com.PostId;
            newcom.Text = "<p>" + inmodel.comment + "</p>";
            newcom.CommentDate = DateTime.Now;
            newcom.ReplyTo = com.CommentId;
            _context.Comments.Add(newcom);
            await _context.SaveChangesAsync();
            CommentViewModel model = new CommentViewModel();
            var post = await _context.Posts.Include(p => p.User).Include(p => p.Category).SingleOrDefaultAsync(p => p.PostID == com.PostId);
            model.post = post;
            model.comments = _context.Comments.Where(p => p.PostId == com.PostId);
            return RedirectToAction("Index");

        }
        [HttpPost]
        public async Task<IActionResult> rate ([FromBody] RatingViewModel[] ratings) //сейчас рейтинг присваивается посту, потому что мне западло прокидывать рейтинги
        {
            double amount = 0;
            foreach (RatingViewModel item in ratings)
            {
                amount += Math.Pow(2, Convert.ToInt32(item.weight));
            }
            double alpha = 1 / amount;
            float result = 0;
            //result += Convert.ToSingle(alpha);
            foreach (RatingViewModel item in ratings)
            {
                result += Convert.ToSingle(Convert.ToInt32(item.rating) * Math.Pow(2, Convert.ToInt32(item.weight)) * alpha);
            }
            Post ratedpost = await _context.Posts.SingleOrDefaultAsync(p => p.PostID == Convert.ToInt32(ratings[0].post));
            ratedpost.rating += result;
            _context.Update(ratedpost);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Post)
                .Include(c => c.User)
                .SingleOrDefaultAsync(m => m.CommentId == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            ViewData["PostId"] = new SelectList(_context.Posts, "PostID", "PostDescr");
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,UserId,Text,CommentDate,PostId,LastModified,ReplyTo")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["PostId"] = new SelectList(_context.Posts, "PostID", "PostDescr", comment.PostId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", comment.UserId);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.SingleOrDefaultAsync(m => m.CommentId == id);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["PostId"] = new SelectList(_context.Posts, "PostID", "PostDescr", comment.PostId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", comment.UserId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommentId,UserId,Text,CommentDate,PostId,LastModified,ReplyTo")] Comment comment)
        {
            if (id != comment.CommentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.CommentId))
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
            ViewData["PostId"] = new SelectList(_context.Posts, "PostID", "PostDescr", comment.PostId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", comment.UserId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Post)
                .Include(c => c.User)
                .SingleOrDefaultAsync(m => m.CommentId == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.Comments.SingleOrDefaultAsync(m => m.CommentId == id);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.CommentId == id);
        }
    }
}
