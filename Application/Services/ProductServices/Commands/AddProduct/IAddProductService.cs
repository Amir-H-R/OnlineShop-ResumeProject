using Application.Services.ProductServices.Common;

namespace Application.Services.ProductServices.Commands.AddProduct
{
    public interface IAddProductService
    {
        ResultDto Execute(ProductDto request);
    }
}
