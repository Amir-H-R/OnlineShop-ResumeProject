using Domain.Entities.Finance;
using Domain.Entities.Products;
using Domain.Entities.Users_n_Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Orders
{
    public class Order : BaseEntity
    {
        public virtual User User { get; set; }
        public long UserId { get; set; }

        public virtual PaymentRequest PaymentRequest { get; set; }
        public virtual long PaymentRequestId { get; set; }

        public OrderState OrderState { get; set; }
        public string Address { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
    public class OrderDetail : BaseEntity
    {
        public virtual Order Order { get; set; }
        public long OrderId { get; set; }

        public virtual Product Product { get; set; }
        public long ProductId { get; set; }

        public int Price { get; set; }
        public int Count { get; set; }

    }
    public enum OrderState
    {
        Processing = 0,
        Canceled = 1,
        Delivered = 2
    }

}
