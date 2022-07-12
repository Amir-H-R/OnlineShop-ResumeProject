namespace Application.Services.ProductServices.Queries.GetCategoryMenu
{
    public class CatMenuDto:CategoryDto
    {
        public List<CatMenuDto> ChildCat { get; set; }
    }
}
