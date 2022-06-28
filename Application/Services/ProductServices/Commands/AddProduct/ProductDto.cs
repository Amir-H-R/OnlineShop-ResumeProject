using Microsoft.AspNetCore.Http;

namespace Application.Services.ProductServices.Commands.AddProduct
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public long CategoryId { get; set; }
        public bool IsDisplayed { get; set; }

        public IList<IFormFile> Images { get; set; }
        public List<ProductFeaturesDto> Features { get; set; }
    }
}
