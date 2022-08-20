using Application.Interface.Context;
using Application.Services.Commands.AddUsers;
using Common;
using Domain.Entities.Users_n_Roles;
using Microsoft.AspNetCore.Identity;
using Store.Common;

namespace Application.Services.Commands.AddUsers
{
    public class AddUserService : IAddUserService
    {
        private readonly IDatabaseContext _context;
        public AddUserService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto<UsersRegistrationResult> Execute(UserDto request)
        {
            var passwordHasher = new PasswordHasher();
            var hashedPassword = passwordHasher.HashPassword(request.Password);
            User user = new User()
            {
                Email = request.Email,
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = hashedPassword,
                LockoutEnabled = false,
            };
            List<UserRoles> userRoles = new List<UserRoles>();
            foreach (var item in request.Roles)
            {
                var role = _context.Roles.Where(p => p.Name == item.Name || p.Id == item.Id).FirstOrDefault();
                userRoles.Add(new UserRoles
                {
                    Role = role,
                    RoleId = role.Id,
                    User = user,
                    UserId = (user.Id)
                });
            }
            user.UserRoles = userRoles;
            _context.Users.Add(user);
            _context.SaveChanges();
            return new ResultDto<UsersRegistrationResult>
            {
                Data = new UsersRegistrationResult()
                {
                    UserRoles = userRoles,
                    UserId = user.Id
                },
                IsSuccess = true,
                Message = "کاربر با موفقیت ثبت شد"
            };
        }
    }
}

public class AddIdentityUser : IAddUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    public AddIdentityUser(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public ResultDto<UsersRegistrationResult> Execute(UserDto userdto)
    {
        try
        {
            User user = new User()
            {
                Email = userdto.Email,
                FullName = userdto.FullName,
                PhoneNumber = userdto.PhoneNumber,
                UserName = userdto.Email,
            };
            var userResult = _userManager.CreateAsync(user, userdto.Password).Result;

            var role = _roleManager.FindByIdAsync(userdto.Roles.FirstOrDefault().Id).Result;
            var roleResult = _userManager.AddToRoleAsync(user, role.Name).Result;
            if (userResult.Succeeded && roleResult.Succeeded)
            {
                //Return Data
                List<UserRoles> userRoles = new List<UserRoles>()
                {
                    new UserRoles
                    {
                    Role = role,
                    RoleId = role.Id,
                    User = user,
                    UserId = (user.Id)
                    }
                };
                return new ResultDto<UsersRegistrationResult>()
                {
                    Data = new UsersRegistrationResult()
                    {
                        UserId = user.Id,
                        UserRoles = userRoles
                    },
                    IsSuccess = true,
                    Message = "Registeration Succussful"
                };
            }
            else
            {
                _userManager.DeleteAsync(user);
                _userManager.RemoveFromRoleAsync(user, role.Name);
                throw new Exception();
            }
        }
        catch
        {
            return new ResultDto<UsersRegistrationResult>()
            {
                IsSuccess = false,
                Message = "Error"
            };
        }


    }
}

