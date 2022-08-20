using Application.Services.Commands.AddUsers;
using Application.Services.Commands.RemoveUser;
using Application.Services.Queries.GetRoles;
using Application.Services.Queries.GetUsers;
using Application.Services.UserService.Commands.UserLogin;
using Application.Services.UserServices.Commands.AddRole;
using Application.Services.UserServices.Commands.EditUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Common.UsersFacade
{
    public interface IUserFacade
    {
        IGetRolesService GetRoles { get; }
        IGetUsersService GetUsersService { get; }
        IUserLoginService UserLogin { get; }

        //AddUserService AddUser { get; }
        AddIdentityUser AddUser { get; }
        RemoveUserService RemoveUser { get; }
        EditUserService EditUser { get; }
        AddRoleService AddRole { get; }
    }
}
