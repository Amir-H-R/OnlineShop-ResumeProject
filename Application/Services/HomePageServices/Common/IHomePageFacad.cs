global using Application.Services.HomePageServices.Common;
using Application.Services.HomePageServices.Commands.AddHomePageImage;
using Application.Services.HomePageServices.Commands.AddSlider;
using Application.Services.HomePageServices.Queries.GetHomePageImagesForSite;
using Application.Services.HomePageServices.Queries.GetSliderForSite;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.HomePageServices.Common
{
    public class HomePageFacad : IHomePageFacad
    {
        private readonly IDatabaseContext _context;
        private readonly IWebHostEnvironment _environment;
        public HomePageFacad(IDatabaseContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        private AddSliderService _addSlider;
        public AddSliderService AddSlider
        {
            get
            {
                return _addSlider = _addSlider ?? new AddSliderService(_context, _environment);
            }
        }

        private AddHomePageImageService _addHomePageImage;
        public AddHomePageImageService AddHomePageImage
        {
            get
            {
                return _addHomePageImage = _addHomePageImage ?? new AddHomePageImageService(_context, _environment);
            }
        }

        private IGetSliderForSiteService _getSliderForSite;
        public IGetSliderForSiteService GetSliderForSite
        {
            get
            {
                return _getSliderForSite = _getSliderForSite ?? new GetSliderForSiteService(_context);
            }
        }
        private IGetHomePageImagesForSiteService _getHomePageImagesForSite;
        public IGetHomePageImagesForSiteService GetHomePageImagesForSite
        {
            get
            {
                return _getHomePageImagesForSite = _getHomePageImagesForSite ?? new GetHomePageImagesForSiteService(_context);
            }
        }
    }

    public interface IHomePageFacad
    {
        AddSliderService AddSlider { get; }
        AddHomePageImageService AddHomePageImage { get; }

        IGetSliderForSiteService GetSliderForSite { get; }
        IGetHomePageImagesForSiteService GetHomePageImagesForSite { get; }
  
    }
}
