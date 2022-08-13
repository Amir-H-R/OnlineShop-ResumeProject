using Application.Interface.Context;
using Common;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Diagnostics;
using System.Net;

namespace Application.Services.Queries.GetUsers
{
    public class GetUsersService : IGetUsersService
    {
        private readonly IDatabaseContext _context;
        public GetUsersService(IDatabaseContext context)
        {
            _context = context;
        }

        public GetUsersResultDto Execute(GetUsersRequestsDto requestsDto)
        {

            var users = _context.Users.Include(p => p.UserRoles).ThenInclude(p => p.Role).AsEnumerable();
            if (!string.IsNullOrWhiteSpace(requestsDto.SearchKey))
            {
                users = users.Where(p => p.Email.Contains(requestsDto.SearchKey) || p.FullName.Contains(requestsDto.SearchKey));
            }
            int rowsCount = 1;
            var roles = users.ToList();
            var res = users.ToPaged(requestsDto.Page, 20, out rowsCount).Select(p => new UserDto
            {
                Email = p.Email,
                FullName = p.FullName,
                PhoneNumber = p.PhoneNumber,
                UserId = p.Id,
                Roles = p.UserRoles.ToList().Select(i=> new RoleDto { Name = i.Role.Name}).ToList()
            }).ToList();

            return new GetUsersResultDto
            {
                Users = res,
                Rows = rowsCount
            };
        }
    }
}
