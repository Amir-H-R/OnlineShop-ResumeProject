using Application.Services.FinancialServices.Commands.RequestPayment;
using Application.Services.FinancialServices.Queries.GetPaymentRequest;
using Application.Services.FinancialServices.Queries.GetPaymentRequestForAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.FinancialServices.Common
{
    public interface IFinancialFacade
    {
        IGetPaymentRequestService GetPaymentRequest { get; }
        IGetPaymentRequestForAdminService GetPaymentRequestForAdmin { get; }
        RequestPaymentService RequestPayment { get; }

    }
    public class FinancialFacade : IFinancialFacade
    {
        private readonly IDatabaseContext _context;
        public FinancialFacade(IDatabaseContext context)
        {
            _context = context;
        }

        private RequestPaymentService _requestPayment;
        public RequestPaymentService RequestPayment
        {
            get
            {
                return _requestPayment = _requestPayment ?? new RequestPaymentService(_context);
            }
        }

        private IGetPaymentRequestService _getPaymentRequest;
        public IGetPaymentRequestService GetPaymentRequest
        {
            get
            {
                return _getPaymentRequest = _getPaymentRequest ?? new GetPaymentRequestService(_context);
            }
        }
        private IGetPaymentRequestForAdminService _getPaymentRequestForAdmin;
        public IGetPaymentRequestForAdminService GetPaymentRequestForAdmin
        {
            get
            {
                return _getPaymentRequestForAdmin = _getPaymentRequestForAdmin ?? new GetPaymentRequestForAdminService(_context);
            }
        }
    }
}
