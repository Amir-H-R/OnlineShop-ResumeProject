using Application.Services.OrderServices.Commands.AddOrder;
using Application.Services.OrderServices.Queries.GetOrdersForSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.OrderServices.Common
{
    public interface IOrderFacade
    {
        IGetOrdersForSiteService GetOrdersForSite { get; }

        AddOrderService AddOrder { get; }
    }


    public class OrderFacade:IOrderFacade
    {
        private readonly IDatabaseContext _context;
        public OrderFacade(IDatabaseContext context)
        {
            _context = context;
        }

        private IGetOrdersForSiteService _getOrdersForSite ;
        public IGetOrdersForSiteService GetOrdersForSite
        {
            get
            {
                return _getOrdersForSite = _getOrdersForSite ?? new GetOrdersForSiteService(_context);
            }
        }
        private AddOrderService _addOrder;
        public AddOrderService AddOrder
        {
            get
            {
                return _addOrder = _addOrder ?? new AddOrderService(_context);
            }
        }
    }
}
