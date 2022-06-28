using Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductServices.Queries.GetProductForSite
{

    public class GetProductForSiteService : IGetProductForSiteService
    {
        private readonly IDatabaseContext _context;
        public GetProductForSiteService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto<ProductDto> Execute(Ordering ordering, string searchKey, long? catId, int pageSize, int currentPage)
        {
            Random random = new Random();
            int totalRows = 0;

            var productQuery = _context.Products
                .Include(p => p.ProductImages).AsQueryable();
            if (catId != null)
            {
                productQuery = productQuery.Where(p => p.CategoryId == catId || p.Category.ParentCategoryId == catId);
            }
            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                productQuery = productQuery.Where(p => p.Name.Contains(searchKey) || p.Brand.Contains(searchKey)).AsQueryable();
            }

            switch (ordering)
            {
                case Ordering.NotOrdered:
                    productQuery = productQuery.OrderByDescending(p => p.Id).AsQueryable();
                    break;
                case Ordering.MostViewed:
                    productQuery = productQuery.OrderByDescending(p => p.ViewsCount).AsQueryable();
                    break;
                case Ordering.TopSelling:
                    productQuery = productQuery.OrderByDescending(p => p.Id).AsQueryable();
                    break;
                case Ordering.MostPopular:
                    productQuery = productQuery.OrderByDescending(p => p.Id).AsQueryable();
                    break;
                case Ordering.Cheapest:
                    productQuery = productQuery.OrderBy(p => p.Price).AsQueryable();
                    break;
                case Ordering.MostExpensive:
                    productQuery = productQuery.OrderByDescending(p => p.Price).AsQueryable();
                    break;
                case Ordering.Newest:
                    productQuery = productQuery.OrderByDescending(p => p.CreateDate).AsQueryable();
                    break;
                default:
                    break;
            }


            var products = productQuery.ToPaged(currentPage, pageSize, out totalRows).Select(p => new SiteProductsDto
            {
                Id = p.Id,
                IMG = p.ProductImages.FirstOrDefault().Src,
                Price = p.Price,
                Star = random.Next(0, 5),
                Title = p.Name
            }).ToList();


            return new ResultDto<ProductDto>
            {
                Data = new ProductDto
                {
                    SiteProducts = products,
                    TotalRows = totalRows,
                },
                IsSuccess = true,
                Message = "success"
            };
        }
    }
}
