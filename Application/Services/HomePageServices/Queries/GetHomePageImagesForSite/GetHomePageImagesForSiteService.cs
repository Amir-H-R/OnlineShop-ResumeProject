using Application.Services.HomePageServices.Commands.AddHomePageImage;
using Domain.Entities.HomePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.HomePageServices.Queries.GetHomePageImagesForSite
{
    public interface IGetHomePageImagesForSiteService
    {
        ResultDto<List<HomePageImageDto>> Execute();
    }
    public class GetHomePageImagesForSiteService : IGetHomePageImagesForSiteService
    {
        private readonly IDatabaseContext _context;
        public GetHomePageImagesForSiteService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<HomePageImageDto>> Execute()
        {
            var imgs = _context.HomePageImages.ToList().OrderByDescending(p => p.Id).Select(p => new HomePageImageDto
            {
                ImageLocation = p.ImageLocation,
                Link = p.Link,
                Src = p.Src
            }).ToList();

            return new ResultDto<List<HomePageImageDto>>
            {
                Data = imgs,
                IsSuccess = true,
                Message = "success"
            };
        }
    }
}
