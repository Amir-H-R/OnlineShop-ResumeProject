using Application.Interface.Context;
using Application.Services.Common.UsersFacade;
using Domain.Entities.Users_n_Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Persistence.Context;

namespace Endpoint.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddScoped<IUserFacade, UserFacade>();
            builder.Services.AddScoped<IDatabaseContext, IdentityDatabaseContext>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OnlineShop", Version = "v1" });
            });
            builder.Services.AddApiVersioning(v =>
            {
                v.ReportApiVersions = true;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



            IConfigurationRoot configuration = new ConfigurationBuilder()
                       .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                       .AddJsonFile("appsettings.json")
                       .Build();
            builder.Services.AddEntityFrameworkSqlServer().AddDbContext<IdentityDatabaseContext>(option => option.UseSqlServer(configuration.GetConnectionString("OnlineShopIdentityDb")));

            builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<IdentityDatabaseContext>().AddDefaultTokenProviders();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.DefaultModelsExpandDepth(-1);
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}