using Application.Services.ProductService.Queries.GetCategory;

public class CategoryDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public bool HasChild { get; set; }
    public long? ParentId { get; set; }
    public ParentCategoryDto Parent { get; set; }
}

