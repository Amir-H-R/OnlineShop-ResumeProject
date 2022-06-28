using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CartServices.Commands.RemoveFromCart
{

    public interface IRemoveFromCartService
    {
        ResultDto Execute(long productId,Guid browserId);
    }
    public class RemoveFromCartService : IRemoveFromCartService
    {
        private readonly IDatabaseContext _context;
        public RemoveFromCartService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long productId, Guid browserId)
        {
            var product = _context.CartItems.Where(p=>p.ProductId == productId && p.Cart.BrowserID == browserId).FirstOrDefault();
            if (product != null)
            {
                product.IsRemoved = true;
                product.RemoveTime = DateTime.Now;
                _context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "محصول از سبد حذف شد"
                };
            }
            else
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "محصول یافت نشد"
                };
            }
        }
    }
}
