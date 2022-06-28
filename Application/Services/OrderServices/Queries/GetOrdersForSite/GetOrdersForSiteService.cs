using Application.Services.OrderServices.Commands.AddOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.OrderServices.Queries.GetOrdersForSite
{
    public interface IGetOrdersForSiteService
    {
        ResultDto<List<OrderDto>> Execute(long userId);
    }
    public class GetOrdersForSiteService : IGetOrdersForSiteService
    {
        private readonly IDatabaseContext _context;
        public GetOrdersForSiteService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<OrderDto>> Execute(long userId)
        {
            var orders = _context.Orders.Include(p => p.OrderDetails).ThenInclude(p=>p.Product).ThenInclude(p=>p.ProductImages).Where(p => p.UserId == userId).OrderByDescending(p => p.Id).ToList().Select(p => new OrderDto
            {
                OrderId = p.Id,
                UserId = p.UserId,
                ProductCount = p.OrderDetails.Select(p => p.Count).Sum(),
                OrderState = p.OrderState,
                PaymentRequestId = p.PaymentRequestId,
                TotalPrice = p.OrderDetails.Where(i=>i.OrderId == p.Id).ToList().Select(o=>o.Price).Sum(),
                ProductIMG = p.OrderDetails.Where(i => i.OrderId == p.Id).OrderByDescending(p=>p.Id).FirstOrDefault()?.Product.ProductImages.FirstOrDefault()?.Src ?? ""
            }).ToList();

            return new ResultDto<List<OrderDto>>
            {
                Data = orders,
                IsSuccess = true,
            };
        }
    }
}
