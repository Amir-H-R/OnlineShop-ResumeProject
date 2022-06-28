using Domain.Entities.Orders;

namespace Application.Services.OrderServices.Commands.AddOrder
{
    public class OrderDto
    {
        public int ProductCount { get; set; }
        public int TotalPrice { get; set; }
        public long OrderId { get; set; }
        public OrderState OrderState { get; set; }
        public long PaymentRequestId { get; set; }
        public long CartId { get; set; }
        public long UserId { get; set; }
        public string Authority { get; set; }
        public string ProductIMG { get; set; }
        public long RefId { get; set; } = 0;
        public List<OrderDetailDto> OrderDetails { get; set; }
    }

}

