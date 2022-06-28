using Application.Interface.Context;
using Domain.Entities.Users_n_Roles;
using FluentValidation;
using Store.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validator.Users
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        private readonly IDatabaseContext _context;
        public LoginValidator(IDatabaseContext context)
        {
            _context = context;
            RuleFor(p => p.Email).Must(EmailValidator).WithMessage("ایمل یا رمز عبور اشتباه وارد شده است");
            RuleFor(p => p.Password).Must(PasswordValidator).WithMessage("ایمل یا رمز عبور اشتباه وارد شده است");
        }

        private bool EmailValidator(string email)
        {
            var user = _context.Users.Where(p => p.Email == email).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool PasswordValidator(string password)
        {
            PasswordHasher hasher = new PasswordHasher();
            var user = _context.Users.ToList();
            foreach (var item in user)
            {
              var pass =   hasher.VerifyPassword(item.Password, password);
                if (pass != false)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
