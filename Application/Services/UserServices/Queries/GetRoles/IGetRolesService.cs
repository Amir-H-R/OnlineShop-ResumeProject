using Common;

namespace Application.Services.Queries.GetRoles
{
    public interface IGetRolesService
    {
        ResultDto<List<RoleDto>> Execute();
    }
}
