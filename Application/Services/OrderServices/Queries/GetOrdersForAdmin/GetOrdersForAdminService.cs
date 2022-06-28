using Application.Services.OrderServices.Commands.AddOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.OrderServices.Queries.GetOrdersForAdmin
{
    //public interface IGetOrdersForAdminService
    //{
    //    Task<ResultDto<List<OrderDto>>> Execute();
    //}
    //public class GetOrdersForAdminService : IGetOrdersForAdminService
    //{
    //    private readonly IDatabaseContext _context;
    //    public GetOrdersForAdminService(IDatabaseContext context)
    //    {
    //        _context = context;
    //    }
    //    public async Task<ResultDto<List<OrderDto>>> Execute()
    //    {
    //    async    var cart = _context.Carts.Where();

    //        async var orders = _context.Orders.Include(p => p.PaymentRequest).ThenInclude(p => p.).ToList().Select(p => new OrderDto
    //        {
    //            OrderId = p.Id,
    //            CartId = p.
    //            });
    //        await orders;
    //    }
        
        
    //}
}
