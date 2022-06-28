using Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.OrderServices.Commands.AddOrder
{
    public interface IAddOrderService
    {
        ResultDto Execute(OrderDto orderDto);
    }
    public class AddOrderService : IAddOrderService
    {
        private readonly IDatabaseContext _context;
        public AddOrderService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(OrderDto orderDto)
        {
            var cart = _context.Carts.Include(p => p.CartItems).ThenInclude(p => p.Product).Where(p => p.Id == orderDto.CartId && p.Finished == false).FirstOrDefault();
            var user = _context.Users.Find(orderDto.UserId);
            var paymentRequest = _context.PaymentRequests.Find(orderDto.PaymentRequestId);

            paymentRequest.Authority = orderDto.Authority;
            paymentRequest.PayDate = DateTime.Now;
            paymentRequest.IsPaid = true;
            paymentRequest.RefId = orderDto.RefId;

            cart.Finished = true;
            

            Order order = new Order()
            {
                Address = "",
                OrderState = OrderState.Processing,
                PaymentRequest = paymentRequest,
                User = user,
            };
            _context.Orders.Add(order);

            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (var item in cart.CartItems)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    Count = item.Count,
                    Price = item.Product.Price,
                    Product = item.Product,
                    Order = order,
                };
                orderDetails.Add(orderDetail);
            }
            _context.OrderDetails.AddRange(orderDetails);
            _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
                Message = ""
            };
        }
    }

}

