using Application.Interface.Context;
using Application.Services.ProductService.Commands.AddCategory;
using Application.Services.ProductService.Queries.GetCategory;
using Application.Services.ProductServices.Commands.AddProduct;
using Application.Services.ProductServices.Queries.GetAllCategories;
using Application.Services.ProductServices.Queries.GetCategoryMenu;
using Application.Services.ProductServices.Queries.GetProductDetailForAdmin;
using Application.Services.ProductServices.Queries.GetProductDetailForSite;
using Application.Services.ProductServices.Queries.GetProductForAdmin;
using Application.Services.ProductServices.Queries.GetProductForSite;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductService.Common
{
    public class ProductFacad : IProductFacad
    {
        private readonly IDatabaseContext _context;
        private readonly IWebHostEnvironment _environment;
        public ProductFacad(IDatabaseContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        private IGetCategoryService _getCategory;
        public IGetCategoryService GetCategory
        {
            get
            {
                return _getCategory = _getCategory ?? new GetCategoryService(_context);
            }
        }
        private IGetAllCategoriesService _getAllCategories;
        public IGetAllCategoriesService GetAllCategories
        {
            get
            {
                return _getAllCategories = _getAllCategories ?? new GetAllCategoriesService(_context);
            }
        }
        private IGetProductForAdminService _getProductForAdmin;
        public IGetProductForAdminService GetProductForAdmin
        {
            get
            {
                return _getProductForAdmin = _getProductForAdmin ?? new GetProductForAdminService(_context);
            }
        }
        private IGetProductDetailForAdminService _getProductDetailForAdmin;
        public IGetProductDetailForAdminService GetProductDetailForAdmin
        {
            get
            {
                return _getProductDetailForAdmin = _getProductDetailForAdmin ?? new GetProductDetailForAdminService(_context);
            }
        }
        private IGetProductForSiteService _getProductForSite;
        public IGetProductForSiteService GetProductForSite
        {
            get
            {
                return _getProductForSite = _getProductForSite ?? new GetProductForSiteService(_context);
            }
        }
        private IGetProductDetailForSiteService _getProductDetailForSite;
        public IGetProductDetailForSiteService GetProductDetailForSite
        {
            get
            {
                return _getProductDetailForSite = _getProductDetailForSite ?? new GetProductDetailForSiteService(_context);

            }
        }
        private IGetCategoryMenu _getCategoryMenu;
        public IGetCategoryMenu GetCategoryMenu
        {
            get
            {
                return _getCategoryMenu = _getCategoryMenu ?? new GetCategoryMenu(_context);
            }
        }

        private AddCategoryService _addCategory;
        public AddCategoryService AddCategoty
        {
            get
            {
                return _addCategory = _addCategory ?? new AddCategoryService(_context);
            }
        }
        private AddProductService _addProduct;
        public AddProductService AddProduct
        {
            get
            {
                return _addProduct = _addProduct ?? new AddProductService(_context, _environment);
            }
        }
    }
}
