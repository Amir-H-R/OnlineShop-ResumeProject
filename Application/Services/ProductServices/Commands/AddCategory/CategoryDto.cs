namespace Application.Services.ProductService.Commands.AddCategory
{
    public class CategoryDto
    {
        public string Name { get; set; } 
        public long? ParentId { get; set; }
    }


}
