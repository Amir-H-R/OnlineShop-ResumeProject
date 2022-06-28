global using Microsoft.EntityFrameworkCore;
global using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductService.Queries.GetCategory
{
    public class GetCategoryService : IGetCategoryService
    {
        private readonly IDatabaseContext _context;
        public GetCategoryService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<CategoryDto>> Execute(long? parentId)
        {
            var category = _context.Categories.Include(p => p.ParentCategory).Include(p => p.SubCategory).Where(p => p.ParentCategoryId == parentId).Select(p =>
                      new CategoryDto
                      {
                          Name = p.Name,
                          Id = p.Id,
                          HasChild = p.SubCategory.Count > 0 ? true : false,
                          Parent = p.ParentCategory != null ? new ParentCategoryDto { Id = p.ParentCategory.Id, Name = p.ParentCategory.Name } : null
                      }
                ).ToList();
            return new ResultDto<List<CategoryDto>>()
            {
                Data = category,
                IsSuccess = true,
                Message = "Success"
            };
        }
    }
}
