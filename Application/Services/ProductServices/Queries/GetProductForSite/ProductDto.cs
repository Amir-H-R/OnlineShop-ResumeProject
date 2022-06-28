namespace Application.Services.ProductServices.Queries.GetProductForSite
{
    public class ProductDto
    {
        public List<SiteProductsDto> SiteProducts { get; set; }
        public int TotalRows { get; set; }
    }
}
