using Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductServices.Queries.GetProductDetailForAdmin
{

    public class GetProductDetailForAdminService : IGetProductDetailForAdminService
    {
        private readonly IDatabaseContext _context;
        public GetProductDetailForAdminService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto<ProductDetailDto> Execute(long Id)
        {
            var product = _context.Products
                 .Include(p => p.Category).ThenInclude(p => p.ParentCategory)
                 .Include(p => p.ProductFeatures).Include(p => p.ProductImages)
                 .Where(p => p.Id == Id)
            .Select(p => new ProductDetailDto
            {
                Category = GetCategory(p.Category),
                Brand = p.Brand,
                Description = p.Description,
                Inventory = p.Inventory,
                Name = p.Name,
                IsDisplayed = p.IsDisplayed,
                Id = p.Id,
                Views = p.ViewsCount,
                Price = p.Price,
                ProductFeatures = p.ProductFeatures.ToList().Select(p => new ProductFeaturesDto
                {
                    Id = p.Id,
                    DisplayName = p.DisplayName,
                    Value = p.Value,
                }).ToList(),
                ProductImages = p.ProductImages.ToList().Select(p => new ProductImagesDto
                {
                    Id = p.Id,
                    Src = p.Src
                }).ToList()
            }).FirstOrDefault();

            return new ResultDto<ProductDetailDto>
            {
                Data = product,
                IsSuccess = true,
                Message = " success"
            };
        }
        private static string GetCategory(Category category)
        {
            string result = category.ParentCategory != null ? $"{category.ParentCategory.Name} -" : "";
            return result += category.Name;
        }
    }
}
