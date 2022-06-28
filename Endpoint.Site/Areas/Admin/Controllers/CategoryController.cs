using Application.Interface.Context;
using Application.Services.ProductService.Commands.AddCategory;
using Application.Services.ProductService.Common;
using Microsoft.AspNetCore.Mvc;


namespace Endpoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IProductFacad _facade;
        public CategoryController(IProductFacad facade)
        {
            _facade = facade;
        }

        [HttpGet]
        public IActionResult Index(long? parentId)
        {
            var category = _facade.GetCategory.Execute(parentId).Data;
            return View(category);
        }

        [HttpGet]
        public IActionResult Add(long? parentId)
        {
            ViewBag.ParentId = parentId;
            return View();
        }

        [HttpPost]
        public IActionResult Add(long? parentId, string name)
        {
          
            ViewBag.ParentId = parentId;
            var category = _facade.AddCategoty.Execute(new CategoryDto
            {
                Name = name,
                ParentId = parentId
            });
            if (parentId == null)
            {
                return Redirect("~/admin/category/");
            }
            return View();
        }
    }
}
