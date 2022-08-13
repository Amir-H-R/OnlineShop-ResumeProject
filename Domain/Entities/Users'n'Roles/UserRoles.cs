using Domain.Entities.BaseEntities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Users_n_Roles
{
    public class UserRoles : IdentityUserRole<string>
    {
        public long Id { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
        public bool IsRemoved { get; set; } = false;
        public DateTime? RemoveTime { get; set; }

        public virtual User User { get; set; }
        //public long UserId { get; set; }

        public virtual Role Role { get; set; }
        //public long RoleId { get; set; }

    }
}
