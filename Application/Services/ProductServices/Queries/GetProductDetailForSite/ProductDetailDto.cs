using Application.Services.ProductServices.Commands.AddProduct;

namespace Application.Services.ProductServices.Queries.GetProductDetailForSite
{
    public class ProductDetailDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int? CartCount { get; set; } = 0;
        public string Description { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public List<ProductFeaturesDto> ProductFeatures { get; set; }
        public List<string> ProductImages { get; set; }
    }

}
