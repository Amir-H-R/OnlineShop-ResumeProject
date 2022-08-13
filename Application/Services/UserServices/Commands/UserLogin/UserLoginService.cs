using Application.Interface.Context;
using Common;
using Microsoft.EntityFrameworkCore;
using Store.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserService.Commands.UserLogin
{

    public class UserLoginService : IUserLoginService
    {
        private readonly IDatabaseContext _context;
        public UserLoginService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto<LoginResultDto> Execute(LoginDto dto)
        {
            PasswordHasher hasher = new PasswordHasher();
            var user = _context.Users.Include(p => p.UserRoles).ThenInclude(p => p.Role).Where(p => p.Email == dto.Email).FirstOrDefault();
            var password = hasher.VerifyPassword(user.PasswordHash, dto.Password);
            if (password == true)
            {
             
                List<string> roles = new List<string>();
                foreach (var item in user.UserRoles)
                {
                    roles.Add(item.Role.Name);
                }
                
                LoginResultDto resultDto = new LoginResultDto()
                {
                    Name = user.FullName,
                    UserId = user.Id,
                    Email = user.Email,
                    Role = roles,
                };
                return new ResultDto<LoginResultDto>
                {
                    Data = resultDto,
                    IsSuccess = true,
                    Message = "Success"
                };
            }
            else
            {
                return new ResultDto<LoginResultDto>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "failed"
                };
            }
        }
    }
}

