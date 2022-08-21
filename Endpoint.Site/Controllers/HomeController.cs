global using Application.Services.HomePageServices.Common;
using Application.Interface.Context;
using Application.Services.ProductService.Common;
using Domain.Entities.Users_n_Roles;
using Endpoint.Site.Models;
using Endpoint.Site.Models.ViewModel.HomePage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Endpoint.Site.Controllers
{
    public class HomeController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly IHomePageFacad _homePageFacad;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IHomePageFacad homePageFacad,UserManager<User> userManager)
        {
            _logger = logger;
            _homePageFacad = homePageFacad;
            _userManager = userManager;
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
        [Authorize]
        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            UserDto userDto = new UserDto()
            {
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                TwoFactorEnabled = user.TwoFactorEnabled,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            };
            return View(userDto);
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