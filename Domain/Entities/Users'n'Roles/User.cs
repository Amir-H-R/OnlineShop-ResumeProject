using Domain.Entities.BaseEntities;
using Domain.Entities.Orders;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Users_n_Roles
{
    public interface Base
    {
        public int MyProperty { get; set; }
    }

    public class User : IdentityUser
    {
        //public long UserId { get; set; }
        public string FullName { get; set; }
        //public string Email { get; set; }
        //public long? PhoneNumber { get; set; }
        //public string Password { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual ICollection<UserRoles> UserRoles { get; set; }
        public ICollection<Order> Orders { get; set; }

        //BaseEntities
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
        public bool IsRemoved { get; set; } = false;
        public DateTime? RemoveTime { get; set; }
    }
}
