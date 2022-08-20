
using Domain.Entities.BaseEntities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Users_n_Roles
{
    public class Role : IdentityRole
    {
        //public long RoleId { get; set; }
        //public string Name { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
        public bool IsRemoved { get; set; } = false;
        public DateTime? RemoveTime { get; set; }

        public virtual ICollection<UserRoles> UserRoles{ get; set; }
        public virtual ICollection<User> Users{ get; set; }
    }
}
