using System;
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

namespace SelfOrg.Components
{
    public class PostTags : ViewComponent
    {
        private readonly ApplicationDbContext _context;
    

        public PostTags(ApplicationDbContext context)
        {
            _context = context;
        }

        public string Invoke(int id)
        {
            var neededpt = _context.PostTags.Where(p => p.PostId == id).Include(p => p.Tag);
            string output = "";
            foreach (PostTag item in neededpt)
            {
                output += item.Tag.TagName+", ";
            }

            return output;
        }
    }
}
