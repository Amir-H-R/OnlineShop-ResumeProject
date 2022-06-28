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

namespace Application.Services.HomePageServices.Commands.AddHomePageImage
{
    public interface IAddHomePageImageService
    {
        ResultDto Execute(HomePageImageDto homeDto);
    }
    public class AddHomePageImageService : IAddHomePageImageService
    {
        private readonly IDatabaseContext _context;
        private readonly IWebHostEnvironment _environment;
        public AddHomePageImageService(IDatabaseContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public ResultDto Execute(HomePageImageDto homeDto)
        {
            var uploadResult = UploadFile(homeDto.file);
            HomePageImage homePage = new HomePageImage()
            {
                Link = homeDto.Link,
                Src = uploadResult.FileNameAddress,
                ImageLocation = homeDto.ImageLocation
            };

            _context.HomePageImages.Add(homePage);
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
                string folder = $@"images\HomePage\Images\";
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
    public class HomePageImageDto
    {
        public IFormFile file { get; set; }
        public string Link { get; set; }
        public ImageLocation ImageLocation { get; set; }
    }
}
