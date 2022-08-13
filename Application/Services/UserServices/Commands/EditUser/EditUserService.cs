using Application.Interface.Context;
using Common;

namespace Application.Services.UserServices.Commands.EditUser
{
    public class EditUserService : IEditUserService
    {
        private readonly IDatabaseContext _context;
        public EditUserService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(UserDto dto, string Id)
        {
            var user = _context.Users.Where(p => p.Id == Id).FirstOrDefault();
            user.Email = dto.Email;
            user.FullName = dto.FullName;
            user.PhoneNumber = dto.PhoneNumber;
            //user.UserRoles
            if (user.LockoutEnabled != true)
            {
                _context.SaveChanges();
                return new ResultDto
                {
                    //Data = new UserDto
                    //{
                    //    Email = dto.Email,
                    //    FullName = dto.FullName,
                    //    PhoneNumber = dto.PhoneNumber,
                    //    //Roles
                    //},
                    IsSuccess = true,
                    Message = "User Edited"
                };
            }
            else
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "Operation Failed"
                };
            }
        }
    }

}
