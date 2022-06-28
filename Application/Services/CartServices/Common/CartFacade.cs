using Application.Services.CartServices.Commands.AddToCart;
using Application.Services.CartServices.Commands.RemoveFromCart;
using Application.Services.CartServices.Queries.GetCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CartServices.Common
{
    public interface ICartFacade
    {
        AddToCartService AddToCart { get; }
        RemoveFromCartService RemoveFromCart { get; }
        AddOrLowerCount AddOrLowerCount { get; }
        IGetCartService GetCart { get; }
    }
    public class CartFacade : ICartFacade
    {
        private readonly IDatabaseContext _context;
        public CartFacade(IDatabaseContext context)
        {
            _context = context;
        }
        private AddToCartService _addToCart;
        public AddToCartService AddToCart
        {
            get
            {
                return _addToCart = _addToCart ?? new AddToCartService(_context);
            }
        }
        private RemoveFromCartService _removeFromCart;
        public RemoveFromCartService RemoveFromCart
        {
            get
            {
                return _removeFromCart = _removeFromCart ?? new RemoveFromCartService(_context);
            }
        }
        private AddOrLowerCount _addOrLowerCount;
        public AddOrLowerCount AddOrLowerCount
        {
            get
            {
                return _addOrLowerCount = _addOrLowerCount ?? new AddOrLowerCount(_context);
            }
        }

        private IGetCartService _getCart;
        public IGetCartService GetCart
        {
            get
            {
                return _getCart = _getCart ?? new GetCartService(_context);
            }
        }
    }
}
