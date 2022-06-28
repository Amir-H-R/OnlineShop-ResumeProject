using Application.Services.HomePageServices.Queries.GetHomePageImagesForSite;
using Application.Services.HomePageServices.Queries.GetSliderForSite;

namespace Endpoint.Site.Models.ViewModel.HomePage
{
    public class HomePageViewModel
    {
        public List<HomeSliderDto> HomeSliders { get; set; }
        public List<SiteImagesDto> SiteImagesDto { get; set; }
    }
}
