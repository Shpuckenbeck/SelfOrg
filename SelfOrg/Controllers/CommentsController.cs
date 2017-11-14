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
            model.comments = _context.Comments.Where(p => p.PostId == id).Include(p => p.User); //возможно, не нужно
            model.commmodel = new CommentsModel();
            model.commmodel.comments = _context.Comments.Where(p => p.PostId == id).Include(p => p.User);
            model.crits = _context.CatCrits.Where(p => p.CategoryId == post.CategoryId).Include(p => p.Category).Include(p => p.Criterion);
            var ratings = _context.Ratings.Where(p => p.PostId == id).Include(p => p.User);
            float sum = 0;
            foreach (Rating item in ratings)
            {
                sum += item.rating*item.User.Weight;
            }
            model.post.rating = sum;
            //--------------------------проверка на доступность оценки---------------------------------
            ClaimsPrincipal currentUser = this.User;
            string userid = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.rateable = true;
            if (post.UserId == userid)
            {
                model.rateable = false;
            }
            else
            {
                bool check = _context.Ratings.Any(p => (p.PostId == post.PostID) && (p.UserId == userid));
                if (check == true)
                {
                    model.rateable = false;
                }
                else model.rateable = true;
            }
            model.userrating = 0;
            if (model.rateable == false)
            {
                foreach (Rating your in ratings)
                {
                    if (your.UserId == userid)
                        model.userrating += your.rating*your.User.Weight;
                }
            }
            //---------------------првоерка на доступность редактирования---------------------------
            if (userid == post.UserId)
            {
                model.editable = true;
            }
            else
            {
                model.editable = false;
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> comment([FromBody] ReplyViewModel inmodel)
        {
           
            Comment newcom = new Comment();
            ClaimsPrincipal currentUser = this.User;
            newcom.UserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            int properid = Convert.ToInt32(inmodel.id);
            newcom.PostId = properid;
            newcom.Text = inmodel.comment;
            newcom.CommentDate = DateTime.Now;
            _context.Comments.Add(newcom);
            await _context.SaveChangesAsync();
            CommentsModel model = new CommentsModel();
            model.comments = _context.Comments.Where(p => p.PostId == properid).Include(p => p.User);
            return PartialView("postcomments", model);
        }
        [HttpPost]
        public async Task<IActionResult> reply([FromBody] ReplyViewModel inmodel) //ответ на комментарий
        {

            Comment newcom = new Comment();
            ClaimsPrincipal currentUser = this.User;
            newcom.UserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            int neededid = Convert.ToInt32(inmodel.id);
            Comment com = await _context.Comments.Where(p => p.CommentId == neededid).SingleOrDefaultAsync();
            newcom.PostId = com.PostId;
            newcom.Text = "<p>" + inmodel.comment + "</p>";
            newcom.CommentDate = DateTime.Now;
            newcom.ReplyTo = com.CommentId;
            _context.Comments.Add(newcom);
            await _context.SaveChangesAsync();
            //CommentViewModel model = new CommentViewModel();
            //var post = await _context.Posts.Include(p => p.User).Include(p => p.Category).SingleOrDefaultAsync(p => p.PostID == com.PostId);
            //model.post = post;
            //model.comments = _context.Comments.Where(p => p.PostId == com.PostId);
            //return RedirectToAction("Index");
            CommentsModel model = new CommentsModel();
            model.comments = _context.Comments.Where(p => p.PostId == com.PostId).Include(p => p.User);
            return PartialView("postcomments", model);

        }
        [HttpPost]
        public async Task<IActionResult> rate ([FromBody] RatingViewModel[] ratings) 
        {
            ClaimsPrincipal currentUser = this.User;
           string userid = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            double amount = 0;
            foreach (RatingViewModel item in ratings)
            {
                amount += Math.Pow(2, Convert.ToInt32(item.weight));
            }
            double alpha = 1 / amount; //рассчёт веса критериев
            float result = 0;
            //result += Convert.ToSingle(alpha);
            foreach (RatingViewModel item in ratings)
            {
                result += Convert.ToSingle(Convert.ToInt32(item.rating) * Math.Pow(2, Convert.ToInt32(item.weight)) * alpha);
                Rating newrate = new Rating();
                newrate.CriterionId = Convert.ToInt16(item.criterion);
                newrate.PostId = Convert.ToInt16(item.post);
                newrate.UserId = userid;
                newrate.rating = Convert.ToSingle(Math.Round(Convert.ToInt32(item.rating) * Math.Pow(2, Convert.ToInt32(item.weight)) * alpha, 3));
                _context.Add(newrate);
                await _context.SaveChangesAsync();
            }
            Math.Round(result, 3);
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
