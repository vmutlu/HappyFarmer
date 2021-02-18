using HappyFarmer.Business.Abstract;
using HappyFarmer.Business.Concrate;
using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.DataAccess.Concrete;
using HappyFarmer.DataAccess.Concrete.EntityFrameWork;
using HappyFarmer.DataAccess.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HappyFarmer.Configuration
{
    public static class AppConfiguration
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("HappyFarmerContext");
            FarmerContext.SetConnectionString(connectionString);
            services.AddDbContext<FarmerContext>(opts => opts.UseMySQL(connectionString, i => i.MigrationsAssembly("HappyFarmer.DataAccess")));

            //services.AddScoped<IProductRepository, EfCoreProductRepository>();
            //services.AddScoped<ICategoryRepository, EfCoreCategoryRepository>();
            //services.AddScoped<IUserRepository, EfCoreUserRepository>();
            //services.AddScoped<ISliderRepository, EfCoreSliderRepository>();
            //services.AddScoped<IAboutUsRepository, EfCoreAboutUsRepository>();
            //services.AddScoped<IAdminMessageRepository, EfCoreAdminMessageRepository>();
            //services.AddScoped<IGeneralSettingsRepository, EfCoreGeneralSettingsRepository>();
            //services.AddScoped<IMessageRepository, EfCoreMessageRepository>();
            //services.AddScoped<IMultipleProductImagesRepository, EfCoreMultipleProductImagesRepository>();
            //services.AddScoped<IGlobalMessageRepository, EfCoreGlobalMessageRepository>();
            //services.AddScoped<IBlogRepository, EfCoreBlogRepository>();
            //services.AddScoped<ICartRepository, EfCoreCartRepository>();
            //services.AddScoped<ISecurityInformationRepository, EfCoreSecurityInformationRepository>();
            //services.AddScoped<IOrderRepository, EfCoreOrderRepository>();
            //services.AddScoped<ICommentRepository, EfCoreCommentRepository>();
            //services.AddScoped<IBannerRepository, EfCoreBannerRepository>();
            //services.AddScoped<IProductCommentRepository, EfCoreProductCommentRepository>();
            //services.AddScoped<ICityRepository, EfCoreCityRepository>();

            //services.AddScoped<IProductService, ProductService>();
            //services.AddScoped<ICategoryService, CategoryService>();
            //services.AddScoped<IUserService, UserService>();
            //services.AddScoped<ISliderService, SliderService>();
            //services.AddScoped<IAboutUsService, AboutUsService>();
            //services.AddScoped<IAdminMessageService, AdminMessageService>();
            //services.AddScoped<IGeneralSettingsService, GeneralSettingsService>();
            //services.AddScoped<IMessageService, MessageService>();
            //services.AddScoped<IMultipleProductImagesService, MultipleProductImagesService>();
            //services.AddScoped<IGlobalMessageService, GlobalMessageService>();
            //services.AddScoped<IBlogService, BlogService>();
            //services.AddScoped<ICartService, CartService>();
            //services.AddScoped<ISecurityInformationService, SecurityInformationService>();
            //services.AddScoped<IOrderService, OrderService>();
            //services.AddScoped<IBannerService, BannerService>();
            //services.AddScoped<ICityService, CityService>();

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
