using Domain.Entities.Users_n_Roles;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserServices.Commands.ConfirmEmail
{


    public interface IConfirmEmailService
    {
        ResultDto Execute(string userId, string token);
    }
    public class ConfirmEmailService : IConfirmEmailService
    {
        private readonly UserManager<User> _userManager;
        public ConfirmEmailService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public ResultDto Execute(string userId, string token)
        {
            try
            {
                var user = _userManager.FindByIdAsync(userId).Result;
                var result = _userManager.ConfirmEmailAsync(user, token).Result;
                if (result.Succeeded)
                {
                    return new ResultDto
                    {
                        IsSuccess = true,
                    };
                }
                else
                {
                    return new ResultDto
                    {
                        IsSuccess = false
                    };
                }
            }
            catch
            {
                return new ResultDto
                {
                    IsSuccess = false
                };
            }
        }
    }
}
