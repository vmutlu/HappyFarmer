using HappyFarmer.DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HappyFarmer.Business.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("HappyFarmerContext");
            FarmerContext.SetConnectionString(connectionString);
            services.AddDbContext<FarmerContext>(opts => opts.UseMySQL(connectionString, i => i.MigrationsAssembly("HappyFarmer.DataAccess")));

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.Name = ".HappyFarmer.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(2);
                options.Cookie.HttpOnly = true; options.Cookie.IsEssential = true;
            });
        }
    }
}
