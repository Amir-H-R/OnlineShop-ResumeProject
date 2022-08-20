using Domain.Entities.BaseEntities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Users_n_Roles
{
    public class UserRoles : IdentityUserRole<string>
    {
        public long Id { get; set; }

        public virtual User User { get; set; }
        public string UserId { get; set; }

        public virtual Role Role { get; set; }
        public string RoleId { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
        public bool IsRemoved { get; set; } = false;
        public DateTime? RemoveTime { get; set; }

    }
}
