using Domain.Entities.Orders;
using Domain.Entities.Users_n_Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Finance
{
    public class PaymentRequest:BaseEntity
    {
        public Guid Guid { get; set; }
        public virtual User User{ get; set; }
        public long UserId{ get; set; }
        public int Amont{ get; set; }
        public bool IsPaid{ get; set; }
        public DateTime? PayDate{ get; set; }


        //ZarinPal
        public string Authority { get; set; } = "0";
        public long RefId { get; set; } = 0;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
