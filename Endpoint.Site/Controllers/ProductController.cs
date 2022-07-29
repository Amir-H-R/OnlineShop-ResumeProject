using Application.Services.ProductService.Common;
using Application.Services.ProductServices.Queries.GetProductForSite;
using Endpoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint.Site.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductFacad _facade;
        private readonly ICookieManager _cookieManager;
        public ProductController(IProductFacad facad, ICookieManager cookieManager)
        {
            _facade = facad;
            _cookieManager = cookieManager;
        }

        public IActionResult Index(Ordering ordering, string searchKey, long? catId = null, int currentPage = 1,int pageSize = 20)
        {
            var products = _facade.GetProductForSite.Execute(ordering, searchKey, catId,pageSize, currentPage).Data;
            return View(products);
        }

        [HttpGet("[Controller]/[Action]/{id}/{name}")]
        public IActionResult Detail(long Id)
        {
            var product = _facade.GetProductDetailForSite.Execute(Id,_cookieManager.GetBrowserId(HttpContext)).Data;
            return View(product);
        }
    }
}
