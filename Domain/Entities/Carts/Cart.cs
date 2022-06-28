using Domain.Entities.Products;
using Domain.Entities.Users_n_Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Carts
{
    public class Cart : BaseEntity
    {
        public virtual User User { get; set; }
        public long? UserId { get; set; }
        public Guid BrowserID { get; set; }
        public bool Finished { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
    public class CartItem : BaseEntity
    {
        public virtual Product Product { get; set; }
        public long ProductId{ get; set; }
        public int Price{ get; set; }
        public int Count{ get; set; }
        public Cart Cart { get; set; }
        public long CartId { get; set; }
    }
}
