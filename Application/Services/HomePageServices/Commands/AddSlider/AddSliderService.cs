using Application.Services.ProductServices.Commands.AddProduct;
using Domain.Entities.HomePage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.HomePageServices.Commands.AddSlider
{
    public interface IAddSliderService
    {
        ResultDto Execute(SliderDto sliderDto);
    }

    public class AddSliderService : IAddSliderService
    {
        private readonly IDatabaseContext _context;
        private readonly IWebHostEnvironment _environment;
        public AddSliderService(IDatabaseContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public ResultDto Execute(SliderDto sliderDto)
        {
            var uploadResult = UploadFile(sliderDto.File);
            Slider slider = new Slider()
            {
                Src = uploadResult.FileNameAddress,
                Link = sliderDto.Link
            };
            _context.Sliders.Add(slider);
            _context.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "success"
            };
        }

        private UploadDto UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string folder = $@"images\HomePage\Slider\";
                var uploadsRootFolder = Path.Combine(_environment.WebRootPath, folder);
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }
                string fileName = DateTime.Now.Ticks.ToString() + file.FileName;
                var filePath = Path.Combine(uploadsRootFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                return new UploadDto
                {
                    FileNameAddress = folder + fileName,
                    Status = true
                };
            }
            return null;
        }
    }

    public class SliderDto
    {
        public string Link { get; set; }
        public IFormFile File { get; set; }
    }
}
