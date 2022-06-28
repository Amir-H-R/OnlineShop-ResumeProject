namespace Application.Services.OrderServices.Commands.AddOrder
{
    public class OrderDetailDto
    {
        public long OrderDetailId { get; set; }
        public int ProductCount { get; set; }
        public string ProductName { get; set; }
        public long ProductId { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
    }

}

