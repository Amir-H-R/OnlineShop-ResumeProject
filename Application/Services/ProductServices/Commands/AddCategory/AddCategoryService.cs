using Application.Interface.Context;
using Common;
using Domain.Entities.Products;

namespace Application.Services.ProductService.Commands.AddCategory
{
    public class AddCategoryService : IAddCategoryService
    {
        private readonly IDatabaseContext _context;
        public AddCategoryService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(CategoryDto dto)
        {
            Category category = new Category()
            {
                Name = dto.Name,
                ParentCategory = GetParent(dto.ParentId),
            };

            _context.Categories.Add(category);
            _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
                Message = "Success"
            };
        }

        private Category GetParent(long? patentId)
        {
            return _context.Categories.Find(patentId);
        }
    }


}
