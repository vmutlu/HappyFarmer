using AutoMapper;
using HappyFarmer.Business.Abstract;
using HappyFarmer.Business.Concrate;
using HappyFarmer.DataAccess;
using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.DataAccess.Concrete;
using HappyFarmer.DataAccess.Concrete.EntityFrameWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace HappyFarmer.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));

            string connectionString = Configuration.GetConnectionString("HappyFarmerContext");
            FarmerContext.SetConnectionString(connectionString);
            services.AddDbContext<FarmerContext>(opts => opts.UseMySQL(connectionString, i => i.MigrationsAssembly("HappyFarmer.DataAccess")));

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Happy Farmer Application Api Swagger",
                    Contact = new OpenApiContact
                    {
                        Name = "Veysel MUTLU",
                        Email = "veysel_mutlu42@hotmail.com",
                        Url = new Uri("http://veyselmutlu.online/")
                    }
                });
            });

            services.AddScoped<IProductRepository, EfCoreProductRepository>();
            services.AddScoped<ICategoryRepository, EfCoreCategoryRepository>();
            services.AddScoped<IMenuRepository, EfCoreMenuRepository>();
            services.AddScoped<IMessageRepository, EfCoreMessageRepository>();
            services.AddScoped<IUserRepository, EfCoreUserRepository>();
            services.AddScoped<IProductCommentRepository, EfCoreProductCommentRepository>();
            services.AddScoped<IGlobalMessageRepository, EfCoreGlobalMessageRepository>();
            services.AddScoped<IAdminMessageRepository, EfCoreAdminMessageRepository>();
            services.AddScoped<IBannerRepository, EfCoreBannerRepository>();
            services.AddScoped<ISliderRepository, EfCoreSliderRepository>();
            services.AddScoped<IUserRepository, EfCoreUserRepository>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGlobalMessageService, GlobalMessageService>();
            services.AddScoped<IAdminMessageService, AdminMessageService>();
            services.AddScoped<IBannerService, BannerService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SeedDatabase.Seed();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Happy Farmer Application Api Swagger");
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Products}/{action=GetAll}");
            });

        }
    }
}
