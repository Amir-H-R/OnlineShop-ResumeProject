using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Commands.RemoveUser
{
    public interface IRemoveUserService
    {
        public ResultDto Execute(long UserId);
    }
}