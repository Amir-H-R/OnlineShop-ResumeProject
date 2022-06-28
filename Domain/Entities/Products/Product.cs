using Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Carts;

namespace Domain.Entities.Products
{
    public class Product : BaseEntityNoId
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public int ViewsCount { get; set; }
        public bool IsDisplayed { get; set; }

        public virtual Category Category { get; set; }
        public long CategoryId { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductFeature> ProductFeatures { get; set; }


        public virtual ICollection<Cart> Carts { get; set; }
    }
}
