using Domain.Entities.BaseEntities;
using Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Users_n_Roles
{
    public class User : BaseEntityNoId
    {
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public long? PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<UserRoles> UserRoles { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
