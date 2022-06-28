namespace Application.Services.ProductServices.Queries.GetProductForSite
{
    public interface IGetProductForSiteService
    {
        ResultDto<ProductDto> Execute(Ordering ordering, string searchKey, long? catId, int pageSize, int currentPage);
    }
}
