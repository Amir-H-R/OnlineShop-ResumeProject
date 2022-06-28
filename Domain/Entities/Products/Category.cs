using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.BaseEntities;

namespace Domain.Entities.Products
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        
        //ساختار درختی
       public virtual Category ParentCategory { get; set; } 
        public long? ParentCategoryId { get; set; }
 
        public virtual ICollection<Category> SubCategory { get; set; }
    }
}
