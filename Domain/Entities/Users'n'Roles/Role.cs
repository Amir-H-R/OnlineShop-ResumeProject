
using Domain.Entities.BaseEntities;

namespace Domain.Entities.Users_n_Roles
{
    public class Role : BaseEntityNoId
    {
        public long RoleId { get; set; }
        public string Name { get; set; }
        public ICollection<UserRoles> UserRoles{ get; set; }
    }
}
