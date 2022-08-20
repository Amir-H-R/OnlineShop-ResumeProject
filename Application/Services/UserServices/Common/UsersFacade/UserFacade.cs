using Application.Interface.Context;
using Application.Services.Commands.AddUsers;
using Application.Services.Commands.RemoveUser;
using Application.Services.Queries.GetRoles;
using Application.Services.Queries.GetUsers;
using Application.Services.UserService.Commands.UserLogin;
using Application.Services.UserServices.Commands.AddRole;
using Application.Services.UserServices.Commands.ConfirmEmail;
using Application.Services.UserServices.Commands.EditUser;
using Domain.Entities.Users_n_Roles;
using Microsoft.AspNetCore.Identity;
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
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public UserFacade(IDatabaseContext context, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
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


        //private AddUserService _addUser;
        //public AddUserService AddUser
        //{
        //    get
        //    {
        //        return _addUser = _addUser ?? new AddUserService(_context);
        //    }
        //}
        private AddIdentityUser _addUser;
        public AddIdentityUser AddUser
        {
            get
            {
                return _addUser = _addUser ?? new AddIdentityUser(_userManager, _roleManager);
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

        private AddRoleService _addRole;
        public AddRoleService AddRole
        {
            get
            {
                return _addRole ?? new AddRoleService(_roleManager);
            }
        }
        private ConfirmEmailService _confirmEmail;
        public ConfirmEmailService ConfirmEmail
        {
            get
            {
                return _confirmEmail ?? new ConfirmEmailService(_userManager);
            }
        }

    }
}
