using Domain.Entities.Users_n_Roles;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserServices.Commands.AddRole
{

    public interface IAddRoleService
    {
        ResultDto Execute(RoleDto roleDto);
    }
    public class AddRoleService : IAddRoleService
    {
        private readonly RoleManager<Role> _roleManager;
        public AddRoleService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public ResultDto Execute(RoleDto roleDto)
        {
            try
            {
                Role role = new Role()
                {
                                        Name = roleDto.Name,
                };
                var result = _roleManager.CreateAsync(role).Result;
                if (result.Succeeded)
                {
                    return new ResultDto
                    {
                        IsSuccess = true,
                        Message = "Role Added"
                    };
                }
                else
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "Failed"
                    };
                }
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
