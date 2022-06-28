namespace Application.Services.ProductService.Queries.GetCategory
{
    public interface IGetCategoryService
    {
        ResultDto<List<CategoryDto>> Execute(long? parentId);
    }
}
