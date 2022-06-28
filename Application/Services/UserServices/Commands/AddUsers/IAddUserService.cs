using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Commands.AddUsers
{
    public interface IAddUserService
    {
        ResultDto<UsersRegistrationResult> Execute(UserDto user);
    }
}

