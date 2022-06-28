using Domain.Entities.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CartServices.Commands.AddToCart
{
    public interface IAddToCartService
    {
        ResultDto Execute(long productId, Guid browserId, string? action, long? userId);
    }
    public class AddToCartService : IAddToCartService
    {
        private readonly IDatabaseContext _context;
        public AddToCartService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long productId, Guid browserId, string? action, long? userId)
        {
            //Check if Cart Exists
            var cart = _context.Carts.Where(p => p.BrowserID == browserId && p.Finished == false || p.UserId == userId).FirstOrDefault();
            if (cart == null)
            {
                Cart newCart = new Cart()
                {
                    BrowserID = browserId,
                    Finished = false,
                    UserId = userId
                    
                };
                _context.Carts.Add(newCart);
                _context.SaveChanges();
                cart = newCart;
            }

            var product = _context.Products.Find(productId);
            var cartItem = _context.CartItems.Include(p => p.Cart).Where(p => p.ProductId == productId && p.CartId == cart.Id).FirstOrDefault();
            if (cartItem != null)
            {
                cartItem.Count++;
                _context.SaveChanges();
            }
            else
            {
                CartItem newItem = new CartItem()
                {
                    Cart = cart,
                    Count = 1,
                    Price = product.Price,
                    Product = product,
                                    };
                _context.CartItems.Add(newItem);
                _context.SaveChanges();
            }

            return new ResultDto
            {
                IsSuccess = true,
                Message = "محصول به سبد اضافه شد"
            };
        }
    }

    public class AddOrLowerCount : IAddToCartService
    {
        private readonly IDatabaseContext _context;
        public AddOrLowerCount(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long productId, Guid browserId, string? action, long? userId)
        {
            var result = _context.Carts.Include(p=>p.CartItems).ThenInclude(p=>p.Product)
                .Where(p => p.BrowserID == browserId && p.Finished == false).FirstOrDefault().CartItems.Where(p => p.Product.Id == productId).FirstOrDefault();
            if (result != null)
            {
                if (action == "Add")
                {
                    result.Count++;
                    _context.SaveChanges();
                    return new ResultDto
                    {
                        IsSuccess = true
                    };
                }
                if (action == "Lower" && result.Count > 1)
                {
                    result.Count--;
                    _context.SaveChanges();
                    return new ResultDto
                    {
                        IsSuccess = true
                    };
                }
            }
            return new ResultDto
            {
                IsSuccess = false
            };
        }
    }

}
