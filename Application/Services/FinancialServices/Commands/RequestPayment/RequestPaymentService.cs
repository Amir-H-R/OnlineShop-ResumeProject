using Domain.Entities.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.FinancialServices.Commands.RequestPayment
{

    public interface IRequestPaymentService
    {
        ResultDto<PaymentRequestDto> Execute(int amount, long userId);
    }
    public class RequestPaymentService : IRequestPaymentService
    {
        private readonly IDatabaseContext _context;
        public RequestPaymentService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto<PaymentRequestDto> Execute(int amount, long userId)
        {
            var user = _context.Users.Find(userId);
            PaymentRequest pay = new PaymentRequest()
            {
                Amont = amount,
                User = user,
                Guid = Guid.NewGuid(),
                IsPaid = false,
              
            };
            _context.PaymentRequests.Add(pay);
            _context.SaveChanges();

            return new ResultDto<PaymentRequestDto>
            {
                Data = new PaymentRequestDto
                {
                    Amount = pay.Amont,
                    Email = user.Email,
                    Guid = pay.Guid,
                    PhoneNumber = user.PhoneNumber,
                    PaymentRequestId = pay.Id
                },
                IsSuccess = true,
            };
        }
    }
}
