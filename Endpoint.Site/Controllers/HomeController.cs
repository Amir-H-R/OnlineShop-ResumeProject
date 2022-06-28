global using Application.Services.HomePageServices.Common;
using Application.Interface.Context;
using Application.Services.ProductService.Common;
using Endpoint.Site.Models;
using Endpoint.Site.Models.ViewModel.HomePage;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Endpoint.Site.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHomePageFacad _homePageFacad;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IHomePageFacad homePageFacad)
        {
            _logger = logger;
            _homePageFacad = homePageFacad;

        }

        public IActionResult Index()
        {
            HomePageViewModel viewModel = new HomePageViewModel()
            {
                HomeSliders = _homePageFacad.GetSliderForSite.Execute().Data,
                SiteImagesDto = _homePageFacad.GetHomePageImagesForSite.Execute().Data,
            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}