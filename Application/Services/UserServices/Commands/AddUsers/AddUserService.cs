using Application.Interface.Context;
using Common;
using Domain.Entities.Users_n_Roles;
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
                Password = hashedPassword,     
                IsActive = true,
            };
            List<UserRoles> userRoles = new List<UserRoles>();
            foreach (var item in request.Roles)
            {
              
                var role = _context.Roles.Find(item.Id);
                userRoles.Add(new UserRoles
                {
                    Role = role,
                    RoleId = role.RoleId,
                    User = user,
                    UserId = (user.UserId)
                }) ;
            }
            user.UserRoles = userRoles;
            _context.Users.Add(user);
            _context.SaveChanges();
            return new ResultDto<UsersRegistrationResult>
            {
                Data = new UsersRegistrationResult()
                {
                    UserId = user.UserId
                },
                IsSuccess = true,
                Message = "کاربر با موفقیت ثبت شد"
            };
        }
    }
}

