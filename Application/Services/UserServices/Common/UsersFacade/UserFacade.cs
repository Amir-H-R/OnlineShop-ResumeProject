using Application.Interface.Context;
using Application.Services.Commands.AddUsers;
using Application.Services.Commands.RemoveUser;
using Application.Services.Queries.GetRoles;
using Application.Services.Queries.GetUsers;
using Application.Services.UserService.Commands.UserLogin;
using Application.Services.UserServices.Commands.EditUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Common.UsersFacade
{
    public class UserFacade : IUserFacade
    {
        private readonly IDatabaseContext _context;
        public UserFacade(IDatabaseContext context)
        {
            _context = context;
        }

        private IGetRolesService _getRoles;
        public IGetRolesService GetRoles
        {
            get
            {
                return _getRoles = _getRoles ?? new GetRolesService(_context);
            }
        }
        private IGetUsersService _getUsersService;
        public IGetUsersService GetUsersService
        {
            get
            {
                return _getUsersService = _getUsersService ?? new GetUsersService(_context);
            }
        }

        private IUserLoginService _userLogin;
        public IUserLoginService UserLogin
        {
            get
            {
                return _userLogin = _userLogin ?? new UserLoginService(_context);
            }
        }


        private AddUserService _addUser;
        public AddUserService AddUser
        {
            get
            {
                return _addUser = _addUser ?? new AddUserService(_context);
            }
        }

        private RemoveUserService _removeUser;
        public RemoveUserService RemoveUser
        {
            get
            {
                return _removeUser = _removeUser ?? new RemoveUserService(_context);
            }
        }

        private EditUserService _editUser;
        public EditUserService EditUser
        {
            get
            {
                return _editUser = _editUser ?? new EditUserService(_context);
            }
        }
    }
}
