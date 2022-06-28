using Application.Services.FinancialServices.Common;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FinancialController : Controller
    {
        private readonly IFinancialFacade _financialFacade;
        public FinancialController(IFinancialFacade financialFacade)
        {
            _financialFacade = financialFacade;
        }

        public IActionResult Index()
        {
            var requests = _financialFacade.GetPaymentRequestForAdmin.Execute().Data;

            return View(requests);
        }
    }
}
