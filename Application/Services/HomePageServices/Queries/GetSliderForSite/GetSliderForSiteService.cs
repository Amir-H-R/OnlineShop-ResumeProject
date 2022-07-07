using Application.Services.HomePageServices.Commands.AddSlider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.HomePageServices.Queries.GetSliderForSite
{
    public interface IGetSliderForSiteService
    {
        ResultDto<List<SliderDto>> Execute();
    }

    public class GetSliderForSiteService : IGetSliderForSiteService
    {
        private readonly IDatabaseContext _context;
        public GetSliderForSiteService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<SliderDto>> Execute()
        {
            var sliders = _context.Sliders.ToList().Select(p => new SliderDto
            {
                Link = p.Link,
                Src = p.Src
            }).ToList();
            return new ResultDto<List<SliderDto>>
            {
                Data = sliders,
                IsSuccess = true,
                Message = "success"
            };
        }
    }
}
