﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SelfOrg.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SelfOrg.Data;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewComponents;
namespace SelfOrg.Components
{
    /// <summary>
    /// ViewComponent, подтягиваюший все теги данного поста. Используется 
    /// в представлении /Comments/viewpost в паршалле posthead
    /// </summary>
    public class PostTags : ViewComponent
    {
        private readonly ApplicationDbContext _context;
    

        public PostTags(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Выбирает все записи из PostTags, где id поста соответствует заданному.
        /// Выводит строку с каждым тегом поста. Теги являются ссылками на поиск по тегам
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IViewComponentResult Invoke(int id)
        {
            var neededpt = _context.PostTags.Where(p => p.PostId == id).Include(p => p.Tag);
            string output = "";
            foreach (PostTag item in neededpt)
            {
                output += "<a href = \"/Posts/tags/" + item.Tag.TagId+"\">"+item.Tag.TagName+" </a>";
            }

            return new HtmlContentViewComponentResult(
               new HtmlString(output));
        }
    }
}
