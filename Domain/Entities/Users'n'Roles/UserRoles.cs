using Domain.Entities.BaseEntities;

namespace Domain.Entities.Users_n_Roles
{
    public class UserRoles : BaseEntityNoId
    {
        public long Id { get; set; }
        public virtual User User { get; set; }
        public long UserId { get; set; }

        public virtual Role Role { get; set; }
        public long RoleId { get; set; }

    }
}
