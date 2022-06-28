using Application.Services.ProductService.Commands.AddCategory;
using Application.Services.ProductService.Queries.GetCategory;
using Application.Services.ProductServices.Commands.AddProduct;
using Application.Services.ProductServices.Queries.GetAllCategories;
using Application.Services.ProductServices.Queries.GetCategoryMenu;
using Application.Services.ProductServices.Queries.GetProductDetailForAdmin;
using Application.Services.ProductServices.Queries.GetProductDetailForSite;
using Application.Services.ProductServices.Queries.GetProductForAdmin;
using Application.Services.ProductServices.Queries.GetProductForSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductService.Common
{
    public interface IProductFacad
    {
        AddCategoryService AddCategoty { get; }
        AddProductService AddProduct { get; }

        IGetCategoryService GetCategory { get; }
        IGetAllCategoriesService GetAllCategories { get; }
        IGetProductForAdminService GetProductForAdmin { get;  }
        IGetProductDetailForAdminService GetProductDetailForAdmin { get; }
        IGetProductForSiteService GetProductForSite { get; }
        IGetProductDetailForSiteService GetProductDetailForSite { get; }
        IGetCategoryMenu GetCategoryMenu { get; }
    }
}
