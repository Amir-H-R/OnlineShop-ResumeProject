using Application.Services.ProductService.Common;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint.Site.ViewComponents
{
  
    public class Search:ViewComponent
    {
        private readonly IProductFacad _facad;
        public Search(IProductFacad facad)
        {
            _facad = facad;

        }

        public IViewComponentResult Invoke()
        {
            var categories = _facad.GetCategoryMenu.Execute().Data;
            return View(viewName:"Search",categories);
        }
    }
}
