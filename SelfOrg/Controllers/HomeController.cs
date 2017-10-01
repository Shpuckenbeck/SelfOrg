﻿using System;
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

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Posts.Include(p => p.Category).Include(p => p.User).OrderByDescending(p => p.PostDate);
            //var applicationDbContext = _context.PostTags.Include(p => p.Post).Include(p => p.Post.Category).Include(p => p.Post.User).Include(p => p.Post.PostTags).Include(p => p.Tag);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> user(string login)
        {
            User user = await _context.User.SingleOrDefaultAsync(p => p.UserName == login);
            IndexViewModel model = new IndexViewModel
            {
                User = user
            };
            return View(model);
        }

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
