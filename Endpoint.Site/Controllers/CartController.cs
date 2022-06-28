using Application.Services.CartServices.Common;
using Endpoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Endpoint.Site.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartFacade _cartFacde;
        private readonly ICookieManager _cookieManager;

        public CartController(ICartFacade cartFacade, ICookieManager cookieManager)
        {
            _cartFacde = cartFacade;
            _cookieManager = cookieManager;
        }
        public IActionResult Index()
        {
            var userId = ClaimUtilities.GetUserId(HttpContext.User);
            var lol = _cookieManager.GetBrowserId(HttpContext);
            var cart = _cartFacde.GetCart.Execute(_cookieManager.GetBrowserId(HttpContext),userId).Data;
            return View(cart);
        }


        public IActionResult Add(long productId)
        {
                     var userId = ClaimUtilities.GetUserId(HttpContext.User);
            var cart = _cartFacde.AddToCart.Execute(productId, _cookieManager.GetBrowserId(HttpContext), null,userId);
            return RedirectToAction("Index");
        }

        public IActionResult Count(long productId, string? countAction)
        {
            
            var result = _cartFacde.AddOrLowerCount.Execute(productId, _cookieManager.GetBrowserId(HttpContext), countAction,null);
          
            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Remove(long productId)
        {
            var result = _cartFacde.RemoveFromCart.Execute(productId, _cookieManager.GetBrowserId(HttpContext));
            var loc = HttpContext.Request.Path;
            var loc2 = HttpContext.Request.Host;
            return RedirectToAction("Index");
        }
    }
}
