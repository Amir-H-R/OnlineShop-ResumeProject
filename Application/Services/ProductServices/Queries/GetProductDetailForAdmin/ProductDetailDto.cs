namespace Application.Services.ProductServices.Queries.GetProductDetailForAdmin
{
    public class ProductDetailDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public int Views { get; set; }
        public bool IsDisplayed { get; set; }
        public string Category { get; set; }
        public List<ProductFeaturesDto> ProductFeatures { get; set; }
        public List<ProductImagesDto> ProductImages { get; set; }
    }
}
