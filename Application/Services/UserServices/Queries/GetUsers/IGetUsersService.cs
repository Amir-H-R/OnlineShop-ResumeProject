using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Queries.GetUsers
{
    public interface IGetUsersService
    {
        GetUsersResultDto Execute(GetUsersRequestsDto requestsDto);
    }
}
