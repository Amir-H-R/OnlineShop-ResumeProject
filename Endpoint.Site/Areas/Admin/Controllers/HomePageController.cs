using Application.Services.HomePageServices.Commands.AddHomePageImage;
using Application.Services.HomePageServices.Commands.AddSlider;
using Domain.Entities.HomePage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Endpoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomePageController : Controller
    {
        private readonly IHomePageFacad _homePageFacad;
        public HomePageController(IHomePageFacad homePageFacad)
        {
            _homePageFacad = homePageFacad;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddSlider()
        {

            return View();
        }
        [HttpPost]
        public IActionResult AddSlider(SliderDto sliderDto)
        {
            var slider = _homePageFacad.AddSlider.Execute(sliderDto);
            return View(slider);
        }

        public IActionResult AddHomePageImage()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult AddHomePageImage(HomePageImageDto dto)
        {
            _homePageFacad.AddHomePageImage.Execute(dto);
            return View();
        }
    }
}
