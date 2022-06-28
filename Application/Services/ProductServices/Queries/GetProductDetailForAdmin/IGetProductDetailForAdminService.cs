namespace Application.Services.ProductServices.Queries.GetProductDetailForAdmin
{
    public interface IGetProductDetailForAdminService
    {
        ResultDto<ProductDetailDto> Execute(long Id);
    }
}
