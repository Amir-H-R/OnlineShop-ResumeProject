using Microsoft.EntityFrameworkCore;
using Application.Interface.Context;
using Application.Services.ProductService.Common;
using Application.Services.Common.UsersFacade;
using Application.Validator.Users;
using Domain.Entities.Users_n_Roles;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

using AutoMapper;
using Persistence.Context;
using System;
using Microsoft.AspNetCore.Hosting;
using Application.Services.HomePageServices.Common;
using Application.Services.CartServices.Common;
using Application.Services.FinancialServices.Common;
using Endpoint.Site.Utilities;
using Common;
using Application.Services.OrderServices.Common;
using Application.Services.MailSender;

var builder = WebApplication.CreateBuilder(args);

//Authorization and Roles
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(UserDefaultRoles.Admin, policy => policy.RequireRole(UserDefaultRoles.Admin));
    options.AddPolicy(UserDefaultRoles.Operator, policy => policy.RequireRole(UserDefaultRoles.Operator));
    options.AddPolicy(UserDefaultRoles.Customer, policy => policy.RequireRole(UserDefaultRoles.Customer));
});

//Authentication and Cookies
builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie(options =>
{
    options.LoginPath = new PathString("/");
    options.ExpireTimeSpan = TimeSpan.FromDays(5);
});

//Facade(s) and services Injection
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddIdentity<User,Role>().AddEntityFrameworkStores<IdentityDatabaseContext>().AddDefaultTokenProviders();
//builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();
builder.Services.AddScoped<IDatabaseContext, IdentityDatabaseContext>();
builder.Services.AddScoped<Endpoint.Site.Utilities.ICookieManager, CookieManager>();
builder.Services.AddScoped<IUserFacade, UserFacade>();
builder.Services.AddScoped<IProductFacad, ProductFacad>();
builder.Services.AddScoped<IHomePageFacad, HomePageFacad>();
builder.Services.AddScoped<ICartFacade, CartFacade>();
builder.Services.AddScoped<IFinancialFacade, FinancialFacade>();
builder.Services.AddScoped<IOrderFacade, OrderFacade>();
builder.Services.AddScoped<IMailSenderService, EmailSenderService>();
builder.Services.AddScoped<IValidator<UserDto>, UserValidator>();
builder.Services.AddScoped<IValidator<LoginDto>, LoginValidator>();
builder.Services.AddControllersWithViews().AddFluentValidation(p =>
{
    p.DisableDataAnnotationsValidation = true;
    //p.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
    // p.RegisterValidatorsFromAssemblyContaining<UserValidator>(includeInternalTypes : true);
});


//Database Connection
IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
//builder.Services.AddEntityFrameworkSqlServer().AddDbContext<DatabaseContext>(option => option.UseSqlServer(configuration.GetConnectionString("OnlineShopDb")));
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<IdentityDatabaseContext>(option => option.UseSqlServer(configuration.GetConnectionString("OnlineShopIdentityDb")));

var app = builder.Build();
//Middlewares
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
       name: "areas",
       pattern: "{area:exists}/{controller=user}/{action=Index}/{id?}");

app.Run();