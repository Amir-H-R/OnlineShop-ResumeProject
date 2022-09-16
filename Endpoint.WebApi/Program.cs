using Application.Interface.Context;
using Application.Services.Common.UsersFacade;
using Domain.Entities.Users_n_Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


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
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}