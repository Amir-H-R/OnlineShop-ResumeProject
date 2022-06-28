using Application.Services.OrderServices.Common;
using Endpoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint.Site.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderFacade _orderFacade;
        public OrdersController(IOrderFacade orderFacade)
        {
            _orderFacade = orderFacade;
        }

        public IActionResult Index()
        {
            var userId = ClaimUtilities.GetUserId(User).Value;
            var orders = _orderFacade.GetOrdersForSite.Execute(userId).Data;
            return View(orders);
        }

    }
}
