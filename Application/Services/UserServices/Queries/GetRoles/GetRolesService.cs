using Application.Interface.Context;
using Common;
using Domain.Entities.Users_n_Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Queries.GetRoles
{
    public class GetRolesService : IGetRolesService
    {
        private readonly IDatabaseContext _context;
        public GetRolesService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<RoleDto>> Execute()
        {
            var res = _context.Roles.ToList().Select(p => new RoleDto
            {
                Id = p.RoleId,
                Name = p.Name,
            }).ToList();
            return new ResultDto<List<RoleDto>>
            {
                Data = res,
                IsSuccess = true,
                Message = ""
            };
        }
    }
}
