using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductServices.Queries.GetCategoryMenu
{
    public class GetCategoryMenu : IGetCategoryMenu
    {
        private readonly IDatabaseContext _context;
        public GetCategoryMenu(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<CatMenuDto>> Execute()
        {
            var categories = _context.Categories
                .Include(p => p.SubCategory)
                .Where(p => p.ParentCategoryId == null)
                .ToList().Select(p => new CatMenuDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    ChildCat = p.SubCategory.ToList().Select(c => new CatMenuDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                    }).ToList()

                }).ToList();

            return new ResultDto<List<CatMenuDto>>
            {
                Data = categories,
                IsSuccess = true,
                Message = "success"
            };
        }
    }
}
