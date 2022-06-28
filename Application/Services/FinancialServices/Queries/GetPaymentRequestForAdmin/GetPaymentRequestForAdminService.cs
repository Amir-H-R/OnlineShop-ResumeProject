using Application.Services.FinancialServices.Commands.RequestPayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.FinancialServices.Queries.GetPaymentRequestForAdmin
{
    public interface IGetPaymentRequestForAdminService
    {
        ResultDto<List<PaymentRequestDto>> Execute();
    }
    public class GetPaymentRequestForAdminService : IGetPaymentRequestForAdminService
    {
        private readonly IDatabaseContext _context;
        public GetPaymentRequestForAdminService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<PaymentRequestDto>> Execute()
        {
            var requests = _context.PaymentRequests.Include(p=>p.User).Include(p=>p.Orders).ToList().Select(p => new PaymentRequestDto
            {
                Amount = p.Amont,
                Email = p.User.Email,
                IsPaid = p.IsPaid,
                PayDate = p.PayDate,
                PaymentRequestId = p.Id,
                PhoneNumber = p.User.PhoneNumber,
                UserId = p.UserId
            }).ToList();
            return new ResultDto<List<PaymentRequestDto>>
            {
                Data = requests,
                IsSuccess = true,
            };

        }
    }
}
