using Application.Services.CartServices.Common;
using Application.Services.FinancialServices.Common;
using Application.Services.OrderServices.Commands.AddOrder;
using Application.Services.OrderServices.Common;
using Dto.Payment;
using Endpoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZarinPal;
using ZarinPal.Class;

namespace Endpoint.Site.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IFinancialFacade _financialFacade;
        private readonly ICartFacade _cartFacade;
        private readonly IOrderFacade _orderFacade;
        private readonly ICookieManager _cookieManager;

        private readonly Payment _payment;
        private readonly Authority _authority;
        private readonly Transactions _transactions;
        public PaymentController(IFinancialFacade financialFacade, ICartFacade cartFacade, ICookieManager cookieManager, IOrderFacade orderFacade)
        {
            _financialFacade = financialFacade;
            _cartFacade = cartFacade;
            _cookieManager = cookieManager;
            _orderFacade = orderFacade;

            var expose = new Expose();
            _payment = expose.CreatePayment();
            _authority = expose.CreateAuthority();
            _transactions = expose.CreateTransactions();
        }

        public async Task<IActionResult> Index()
        {
            long? userId = ClaimUtilities.GetUserId(User);
            var browserId = _cookieManager.GetBrowserId(HttpContext);
            var cart = _cartFacade.GetCart.Execute(browserId,userId).Data;

            if (cart.PriceSum > 0)
            {
                var paymentRequest = _financialFacade.RequestPayment.Execute(cart.PriceSum,userId.Value);

                //Redirect to payment gateway
                var result = await _payment.Request(new DtoRequest()
                {
                    Mobile = paymentRequest.Data.PhoneNumber.ToString(),
                    CallbackUrl = $"https://localhost:44380/Payment/Verify?guid={paymentRequest.Data.Guid}",
                    Description = "پرداخت فاکتور شماره:" + paymentRequest.Data.PaymentRequestId,
                    Email = paymentRequest.Data.Email,
                    Amount = paymentRequest.Data.Amount,
                    MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
                }, ZarinPal.Class.Payment.Mode.sandbox);
                return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}");
            }

            return View();
        }

        public async Task<IActionResult> Verify(Guid guid, string authority, string status)
        {
            var paymentAmount = _financialFacade.GetPaymentRequest.Execute(guid).Data.Amount;

            var verification = await _payment.Verification(new DtoVerification
            {
                Amount = paymentAmount,
                MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                Authority = authority
            }, Payment.Mode.sandbox);

            //درصورت کار نکردن متد تایید
            //var client = new RestClient("https://www.zarinpal.com/pg/rest/WebGate/PaymentVerification.json");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.POST);
            //request.AddHeader("Content-Type", "application/json");
            //request.AddParameter("application/json", $"{{\"MerchantID\" :\"{merchendId}\",\"Authority\":\"{Authority}\",\"Amount\":\"{10000}\"}}", ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);
            //VerificationPayResultDto verification = JsonConvert.DeserializeObject<VerificationPayResultDto>(response.Content);

            long? userId = ClaimUtilities.GetUserId(User);
            Guid browserId = _cookieManager.GetBrowserId(HttpContext);
            long cartId = _cartFacade.GetCart.Execute(browserId, userId).Data.CartId;
            long paymentRequestId = _financialFacade.GetPaymentRequest.Execute(guid).Data.PaymentRequestId;

            if (verification.Status == 100)
            {
                OrderDto orderDto = new OrderDto()
                {
                    Authority = authority,
                    RefId = verification.RefId,
                    UserId = userId.Value,
                    CartId = cartId,
                    PaymentRequestId = paymentRequestId
                };
                 _orderFacade.AddOrder.Execute(orderDto);
                return RedirectToAction("Index", "Orders");
            }
            else
            {

            }
            return View();
}
}
}



