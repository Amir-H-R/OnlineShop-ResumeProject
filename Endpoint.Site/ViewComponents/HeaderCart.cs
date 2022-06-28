using Application.Services.CartServices.Common;
using Endpoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint.Site.ViewComponents
{
    public class HeaderCart : ViewComponent
    {
        private readonly ICartFacade _cartFacade;
        private readonly CookieManager _cookie;

        public HeaderCart(ICartFacade cartFacade)
        {
            _cartFacade = cartFacade;
            _cookie = new CookieManager();
        }

        public IViewComponentResult Invoke()
        {
            var userId = ClaimUtilities.GetUserId(HttpContext.User);
            var cart = _cartFacade.GetCart.Execute(_cookie.GetBrowserId(HttpContext), userId).Data;
            return View(viewName: "HeaderCart", cart);
        }
    }
}
