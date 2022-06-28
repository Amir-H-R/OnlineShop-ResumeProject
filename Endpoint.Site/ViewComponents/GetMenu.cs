using Application.Services.ProductService.Common;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint.Site.ViewComponents
{

    public class GetMenu:ViewComponent
    {
        private readonly IProductFacad _facad;
        public GetMenu(IProductFacad facad)
        {
            _facad = facad;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _facad.GetCategoryMenu.Execute().Data;
            return View(viewName:"GetMenu",categories) ;
        }
    }
}
