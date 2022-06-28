using Domain.Entities.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductServices.Commands.AddProduct
{

    public class AddProductService : IAddProductService
    {
        private readonly IDatabaseContext _context;
        private readonly IWebHostEnvironment _environment;
        public AddProductService(IDatabaseContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public ResultDto Execute(ProductDto request)
        {
            try
            {
                Product product = new Product()
                {
                    Brand = request.Brand,
                    Name = request.Name,
                    IsDisplayed = true,
                    Inventory = request.Inventory,
                    CategoryId = request.CategoryId,
                    Description = request.Description,
                    Price = request.Price,
                };
                _context.Products.Add(product);

                List<ProductFeature> features = new List<ProductFeature>();
                foreach (var item in request.Features)
                {
                    features.Add(new ProductFeature
                    {
                        DisplayName = item.DisplayName,
                        Value = item.Value,
                        Product = product
                    });
                }
                _context.ProductFeatures.AddRange(features);

                IList<ProductImage> images = new List<ProductImage>();
                foreach (var item in request.Images)
                {
                    var uploadResult = UploadFile(item);
                    images.Add(new ProductImage
                    {
                        Product = product,
                        Src = uploadResult.FileNameAddress
                    });
                }
                _context.ProductImages.AddRange(images);
                _context.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "کالا با موفقیت ثبت شد"
                };
            }
            catch
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "کالا ثبت نشد"
                };
            }
        }

        private UploadDto UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string folder = $@"images\ProductImages\";
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
}
