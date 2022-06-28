using Application.Interface.Context;
using Common;

namespace Application.Services.Commands.RemoveUser
{
    public class RemoveUserService : IRemoveUserService
    {
        private readonly IDatabaseContext _context;
        public RemoveUserService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(long UserId)
        {
            try
            {
                var user = _context.Users.Find(UserId);
                user.RemoveTime = DateTime.Now;
                user.IsRemoved = true;

                _context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "Success"
                };
            }
            catch
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "Failed"
                };
            }

        }
    }
}