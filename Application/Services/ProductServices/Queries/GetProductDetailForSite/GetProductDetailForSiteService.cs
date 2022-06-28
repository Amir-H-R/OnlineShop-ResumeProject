using Application.Services.ProductServices.Commands.AddProduct;
using Domain.Entities.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductServices.Queries.GetProductDetailForSite
{

    public class GetProductDetailForSiteService : IGetProductDetailForSiteService
    {
        private readonly IDatabaseContext _context;
        public GetProductDetailForSiteService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto<ProductDetailDto> Execute(long Id,Guid browserId)
        {
            var products = _context.Products.Include(p => p.Carts).ThenInclude(p => p.CartItems)
                .Include(p => p.ProductImages).Include(p => p.ProductFeatures)
                .Include(p => p.Category).ThenInclude(p => p.ParentCategory)
                .Where(p => p.Id == Id).FirstOrDefault();

          
            int? carts =_context.Carts.Include(p=>p.CartItems).ToList().Where(p => p.BrowserID == browserId).FirstOrDefault()?.CartItems.ToList().Where(p => p.ProductId == Id).FirstOrDefault()?.Count  ;
         

            var result = new ProductDetailDto
            {
                Id = products.Id,
               CartCount = carts,
                Brand = products.Brand,
                Description = products.Description,
                Price = products.Price,
                Inventory = products.Inventory,
                Title = products.Name,
                Category = $"{products.Category.Name } - {products.Category.ParentCategory.Name}",
                ProductFeatures = products.ProductFeatures.Select(p => new ProductFeaturesDto { DisplayName = p.DisplayName, Value = p.Value }).ToList(),
                ProductImages = products.ProductImages.Select(p => p.Src).ToList(),
            };
            products.ViewsCount++;
            _context.SaveChanges();

            return new ResultDto<ProductDetailDto>
            {
                Data = result,
                IsSuccess = true,
                Message = "success"
            };
        }
    }

}
