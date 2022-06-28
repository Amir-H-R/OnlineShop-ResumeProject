using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductServices.Queries.GetProductForAdmin
{
    public class GetProductForAdminService : IGetProductForAdminService
    {
        private readonly IDatabaseContext _context;
        public GetProductForAdminService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto<ProductListDto> Execute(int pageSize = 20, int page = 1)
        {
            int rowsCount = 0;
            var product = _context.Products.Include(p => p.Category)
                .ToPaged(page, pageSize, out rowsCount)
                .Select(p => new ProductDto
                {
                    Brand = p.Brand,
                    Category = p.Category.Name,
                    Name = p.Name,
                    Description = p.Description,
                    Id = p.Id,
                    Inventory = p.Inventory,
                    IsDisplayed = p.IsDisplayed,
                    Price = p.Price
                }).ToList() ;

            return new ResultDto<ProductListDto>
            {
                Data = new ProductListDto()
                {
                    Products = product,
                    CurrentPage = page,
                    PageSize = pageSize,
                    RowCount = rowsCount
                },
                IsSuccess = true,
                Message = "success"
            };
        }
    }
}
