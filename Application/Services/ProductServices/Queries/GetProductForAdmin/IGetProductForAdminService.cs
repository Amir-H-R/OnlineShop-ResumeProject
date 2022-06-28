namespace Application.Services.ProductServices.Queries.GetProductForAdmin
{
    public interface IGetProductForAdminService
    {
        ResultDto<ProductListDto> Execute(int pageSize = 20, int page = 1);
    }


}
