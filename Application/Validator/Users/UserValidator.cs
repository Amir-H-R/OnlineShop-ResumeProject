using Application.Interface.Context;
using Domain.Entities.Users_n_Roles;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Validator.Users
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        private readonly IDatabaseContext _context;
        public UserValidator(IDatabaseContext context)
        {
            _context = context;
            RuleFor(p => p.PhoneNumber).NotEmpty().WithMessage("لطفا شماره تلفن را وارد کنید");
            RuleFor(p => p.PhoneNumber).Must(PhonenumberValidator).WithMessage("این شماره تلفن قبلا ثبت شده");
            RuleFor(p => p.PhoneNumber.ToString()).Matches(@"^(\+989|9|09)(10|11|12|13|14|15|16|17|18|19|90|91|92|93|94|03|30|33|32|35|36|37|38|39|32|00|01|02|03|04|05|41|20|21|22|31)\d{7}").WithMessage("شماره تلفن معتبر نیست").WithName("PhoneNumber");
            RuleFor(p => p.FullName).NotEmpty().WithMessage("لطفا نام را وارد کنید");
            RuleFor(p => p.FullName).MaximumLength(50).WithMessage("نام وارد شده معتبر نمیباشد");
            RuleFor(p => p.Email).EmailAddress().WithMessage("ایمیل وارد شده معتبر نمیباشد");
            RuleFor(p => p.Email).NotEmpty().WithMessage("لطفا ایمیل را وارد کنید");
            RuleFor(p => p.Email).Must(EmailValidator).WithMessage("این ایمیل قبلا ثبت شده");
            RuleFor(p => p.Password).NotEmpty().WithMessage("لطفا پسورد را وارد کنید");
            RuleFor(p => p.Password).MaximumLength(25).WithMessage("پسورد نباید بیشتر از 25 حرف باید");
            RuleFor(p => p.Password).MinimumLength(8).WithMessage("پسورد نباید کمتر از 8 حرف باشد");
            RuleFor(p => p.RePassword).Equal(p => p.Password).WithMessage("پسورد مطابقت ندارد");
        }
            
        private bool EmailValidator(string email)
        {
            //UserDto user = new UserDto();
            // email = user.Email;
            var emaill = _context.Users.Where(p => p.Email == email).FirstOrDefault();
            if (email == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool PhonenumberValidator(string phonenumber)
        {
            //UserDto user = new UserDto();
            // email = user.Email;
            var emaill = _context.Users.Where(p => p.PhoneNumber == phonenumber).FirstOrDefault();
            if (phonenumber == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}