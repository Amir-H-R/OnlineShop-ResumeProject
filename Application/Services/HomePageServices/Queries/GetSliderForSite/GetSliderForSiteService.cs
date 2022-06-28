using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.HomePageServices.Queries.GetSliderForSite
{
    public interface IGetSliderForSiteService
    {
        ResultDto<List<HomeSliderDto>> Execute();
    }

    public class GetSliderForSiteService : IGetSliderForSiteService
    {
        private readonly IDatabaseContext _context;
        public GetSliderForSiteService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<HomeSliderDto>> Execute()
        {
            var sliders = _context.Sliders.ToList().Select(p => new HomeSliderDto
            {
                Link = p.Link,
                Src = p.Src
            }).ToList();
            return new ResultDto<List<HomeSliderDto>>
            {
                Data = sliders,
                IsSuccess = true,
                Message = "success"
            };
        }
    }

    public class HomeSliderDto
    {
        public string Src { get; set; }
        public string Link { get; set; }
    }
}
