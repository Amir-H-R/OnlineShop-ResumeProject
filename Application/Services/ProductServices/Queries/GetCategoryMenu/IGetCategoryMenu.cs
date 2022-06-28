namespace Application.Services.ProductServices.Queries.GetCategoryMenu
{
    public interface IGetCategoryMenu
    {
        ResultDto<List<CatMenuDto>> Execute();
    }
}
