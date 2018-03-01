

namespace SelfOrg
{
    //не используется в данной имплементации

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
