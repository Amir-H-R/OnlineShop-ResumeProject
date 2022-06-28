namespace Application.Services.ProductServices.Queries.GetProductDetailForSite
{
    public interface IGetProductDetailForSiteService
    {
        ResultDto<ProductDetailDto> Execute(long Id,Guid browserId);
    }

}
