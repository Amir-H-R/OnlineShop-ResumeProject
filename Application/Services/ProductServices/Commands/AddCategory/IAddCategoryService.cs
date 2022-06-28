using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductService.Commands.AddCategory
{
    public interface IAddCategoryService
    {
        public ResultDto Execute(CategoryDto dto);
    }


}
