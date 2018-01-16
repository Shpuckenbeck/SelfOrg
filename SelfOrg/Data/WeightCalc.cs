using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SelfOrg.Models;
using SelfOrg.Models.AccountViewModels;
using SelfOrg.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using SelfOrg.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SelfOrg.Data
{
    public class WeightCalc
    {
       
        private readonly ApplicationDbContext _context;
        public WeightCalc(ApplicationDbContext context)
        {
            _context = context;
        }

        public async void Calculate()
        {
            var users = _context.Users.ToList();
            foreach (User user in users)
            {
                var level = _context.Multipliers.SingleOrDefault(p => (p.Lower < user.rating) && (p.Higher > user.rating)); //находим вес голоса, который соответствуе его рейтингу
                user.Weight = level.Mul;                
            }
            _context.Users.UpdateRange(users);
            await _context.SaveChangesAsync();
        }
    }
}
