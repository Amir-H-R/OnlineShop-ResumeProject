using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.FinancialServices.Commands.RequestPayment;

namespace Application.Services.FinancialServices.Queries.GetPaymentRequest
{
    public interface IGetPaymentRequestService
    {
        ResultDto<PaymentRequestDto> Execute(Guid guid);
    }

    public class GetPaymentRequestService : IGetPaymentRequestService
    {
        private readonly IDatabaseContext _context;
        public GetPaymentRequestService(IDatabaseContext context)
        {
            _context = context;
        }
         
        public ResultDto<PaymentRequestDto> Execute(Guid guid)
        {
            var request = _context.PaymentRequests.Where(p => p.Guid == guid).Select(p=> new PaymentRequestDto
            {
                
                Amount = p.Amont,
                PaymentRequestId = p.Id
            }).FirstOrDefault();
            if (request != null)
            {
                return new ResultDto<PaymentRequestDto>
                {
                    Data = request,
                    IsSuccess = true
                };
            }
            else
            {
                throw new Exception("request not found");
            }
        }
    }
}
