using Application.Interface.Context;
using Application.Services.ProductService.Common;
using Application.Services.ProductServices.Commands.AddProduct;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Endpoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductFacad _facade;
        public ProductController(IProductFacad facade)
        {
            _facade = facade;
        }
        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            var product = _facade.GetProductForAdmin.Execute(pageSize, page).Data;
            return View(product);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var categories = _facade.GetAllCategories.Execute().Data;
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductDto dto, List<ProductFeaturesDto> features)
        {
            List<IFormFile> images = new List<IFormFile>();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                images.Add(file);
            }
            dto.Images = images;
            dto.Features = features;

            var product = _facade.AddProduct.Execute(dto);
            return Json(product);
        }

        [HttpGet]
        public IActionResult Detail(long Id) 
        {
            var product = _facade.GetProductDetailForAdmin.Execute(Id).Data;
            return View(product);
        }
    }
}
