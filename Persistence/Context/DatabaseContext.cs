using Application.Interface.Context;
using Common;
using Domain.Entities.Carts;
using Domain.Entities.Finance;
using Domain.Entities.HomePage;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using Domain.Entities.Users_n_Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<HomePageImage> HomePageImages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<PaymentRequest> PaymentRequests { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);
            ApplyQueryFilter(modelBuilder);
            //یکتا بودن ایمیل
            modelBuilder.Entity<User>().HasIndex(p => p.Email).IsUnique();

            modelBuilder.Entity<Order>().HasOne(p => p.User).WithMany(p => p.Orders).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Order>().HasOne(p => p.PaymentRequest).WithMany(p => p.Orders).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ProductImage>().HasOne(p => p.Product).WithMany(p => p.ProductImages);

            //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            //{
            //    if (entityType.ClrType.GetCustomAttributes(typeof(AuditableAttribute_Ins), true).Length > 0)
            //    {
            //        modelBuilder.Entity(entityType.Name).Property<int>("InsUserId");
            //        modelBuilder.Entity(entityType.Name).Property<DateTime>("InsertTime").HasDefaultValueSql("getDate()");
            //        modelBuilder.Entity<ProductImage>().HasOne(p => p.Product).WithMany(p => p.ProductImages);
            //    }
            //}
        }

        protected void ApplyQueryFilter(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Role>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<UserRoles>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Category>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<ProductFeature>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<ProductImage>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Slider>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<HomePageImage>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Cart>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<CartItem>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<PaymentRequest>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Order>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<OrderDetail>().HasQueryFilter(p => !p.IsRemoved);
        }

        protected void SeedData(ModelBuilder modelBuilder)
        {
            //نقش های پیشفرض انتیتی رول
            modelBuilder.Entity<Role>().HasData(new Role { Id = Guid.NewGuid().ToString(), Name = nameof(UserDefaultRoles.Admin), IsRemoved = false, CreateDate = DateTime.Now });
            modelBuilder.Entity<Role>().HasData(new Role { Id = Guid.NewGuid().ToString(), Name = nameof(UserDefaultRoles.Operator), IsRemoved = false, CreateDate = DateTime.Now });
            modelBuilder.Entity<Role>().HasData(new Role { Id = Guid.NewGuid().ToString(), Name = nameof(UserDefaultRoles.Customer), IsRemoved = false, CreateDate = DateTime.Now });

        }
    }


    public class IdentityDatabaseContext : IdentityDbContext<User, Role, string,IdentityUserClaim<string>,UserRoles,IdentityUserLogin<string>,IdentityRoleClaim<string>,IdentityUserToken<string>>
    {
        public IdentityDatabaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<HomePageImage> HomePageImages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<PaymentRequest> PaymentRequests { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplyQueryFilter(modelBuilder);

            modelBuilder.Entity<User>().HasIndex(p => p.Email).IsUnique();

            modelBuilder.Entity<Order>().HasOne(p => p.User).WithMany(p => p.Orders).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Order>().HasOne(p => p.PaymentRequest).WithMany(p => p.Orders).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ProductImage>().HasOne(p => p.Product).WithMany(p => p.ProductImages);

        }

        protected void ApplyQueryFilter(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Role>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<UserRoles>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Category>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<ProductFeature>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<ProductImage>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Slider>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<HomePageImage>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Cart>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<CartItem>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<PaymentRequest>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Order>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<OrderDetail>().HasQueryFilter(p => !p.IsRemoved);
        }
    }
}
