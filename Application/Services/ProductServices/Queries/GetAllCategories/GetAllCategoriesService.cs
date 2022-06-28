﻿namespace Application.Services.ProductServices.Queries.GetAllCategories
{
    public class GetAllCategoriesService : IGetAllCategoriesService
    {
        private readonly IDatabaseContext _context;
        public GetAllCategoriesService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<AllCategoriesDto>> Execute()
        {
            var categories = _context.Categories
             .Include(p => p.ParentCategory)
                .Where(p => p.ParentCategoryId != null).ToList()
                .Select(p => new AllCategoriesDto { Id = p.Id, Name = $"{p.ParentCategory.Name} - {p.Name}" }).ToList();

            return new ResultDto<List<AllCategoriesDto>>()
            {
                Data = categories,
                IsSuccess = true,
                Message = "Success"
            };
        }
    }
}
