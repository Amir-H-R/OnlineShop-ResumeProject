namespace Application.Services.ProductServices.Queries.GetProductForAdmin
{
    public class ProductListDto
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int RowCount{ get; set; }
        public List<ProductDto> Products{ get; set; }
    }


}
