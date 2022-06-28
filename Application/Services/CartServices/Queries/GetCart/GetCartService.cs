using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CartServices.Queries.GetCart
{

    public interface IGetCartService
    {
        ResultDto<CartDto> Execute(Guid browserId, long? userId);
    }
    public class GetCartService : IGetCartService
    {
        private readonly IDatabaseContext _context;
        public GetCartService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto<CartDto> Execute(Guid browserId, long? userId)
        {
            var cart = _context.Carts.Include(p => p.CartItems)
                .ThenInclude(p => p.Product).ThenInclude(p => p.ProductImages)
                .Where(p => p.BrowserID == browserId && p.Finished == false || p.UserId == userId && p.Finished == false).OrderByDescending(p => p.Id).FirstOrDefault();

            if (cart == null)
            {
                return new ResultDto<CartDto>()
                {
                    Data = new CartDto()
                    {
                        CartItems = new List<CartItemDto>()
                    },
                    IsSuccess = false,
                };
            }

            CartDto cartItems = new CartDto();
            if (cart != null)
            {
                var user = _context.Users.Find(userId);
                cart.User = user;
                cart.BrowserID = browserId;
                _context.SaveChanges();

                cartItems = new CartDto
                {
                    CartId = cart.Id,
                    PriceSum = cart.CartItems.Sum(p => p.Price * p.Count),
                    ProductCount  = cart.CartItems.Count(),

                    CartItems = cart.CartItems.Select(p => new CartItemDto
                    {
                        ProductName = p.Product.Name,
                        ProductId = p.Product.Id,
                        ProductImg = p.Product.ProductImages.FirstOrDefault().Src,
                        Price = p.Price,
                        Count = p.Count,
                    }).ToList()
                };
            }

            return new ResultDto<CartDto>
            {
                Data = cartItems,
                IsSuccess = true,
            };
        }
    }

    public class CartDto
    {
        public long CartId { get; set; }
        public int ProductCount { get; set; }
        public int PriceSum { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }

    public class CartItemDto
    {
        public string ProductName { get; set; }
        public string ProductImg { get; set; }
        public long ProductId { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
    }
}
