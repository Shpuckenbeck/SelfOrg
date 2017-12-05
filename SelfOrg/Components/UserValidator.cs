using FluentValidation;
using SelfOrg.Models;
using System.Linq;
using SelfOrg.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SelfOrg.Services;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Claims;
using System.Diagnostics;

namespace SelfOrg
{
    //public class UserValidator : AbstractValidator<User>
    //{
        
    //    public UserValidator()
    //    {
    //        RuleFor(x => x.displayedname).Must(UniqueNickname).WithMessage("Данный никнейм уже занят");
    //    }

    //    private bool UniqueNickname (string name)
    //    {
    //        var options = new DbContextOptionsBuilder<ApplicationDbContext>();
    //        options.UseSqlServer(Configuration.GetConnectionStringSecureValue("DefaultConnection"));
    //        _context = new ApplicationDbContext(options.Options);
    //        bool used = _context.User.Any(p => p.displayedname.ToLower() == name.ToLower());
    //        return !used;
    //    }
    //}
}
