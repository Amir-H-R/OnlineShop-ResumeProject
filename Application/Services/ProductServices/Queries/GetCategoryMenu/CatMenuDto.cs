namespace Application.Services.ProductServices.Queries.GetCategoryMenu
{
    public class CatMenuDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<CatMenuDto> ChildCat { get; set; }
    }
}
