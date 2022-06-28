global using Application.Interface.Context;
using Domain.Entities.Carts;
using Domain.Entities.Finance;
using Domain.Entities.HomePage;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using Domain.Entities.Users_n_Roles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Context
{
    public interface IDatabaseContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<UserRoles> UserRoles { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductFeature> ProductFeatures { get; set; }
        DbSet<ProductImage> ProductImages { get; set; }
        DbSet<Slider> Sliders { get; set; }
        DbSet<Cart> Carts { get; set; }
        DbSet<CartItem> CartItems { get; set; }
        DbSet<HomePageImage> HomePageImages { get; set; }
        DbSet<PaymentRequest>   PaymentRequests { get; set; }
         DbSet<Order> Orders { get; set; }
         DbSet<OrderDetail> OrderDetails { get; set; }
        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}