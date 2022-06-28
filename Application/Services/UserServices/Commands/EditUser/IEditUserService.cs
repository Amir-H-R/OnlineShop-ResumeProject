using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserServices.Commands.EditUser
{
    public interface IEditUserService
    {
        ResultDto Execute(UserDto dto, long Id);
    }

}
