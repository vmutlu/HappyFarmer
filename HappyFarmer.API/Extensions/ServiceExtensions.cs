using HappyFarmer.Business.Abstract;
using HappyFarmer.Business.Concrate;
using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.DataAccess.Concrete.EntityFrameWork;
using Microsoft.Extensions.DependencyInjection;

namespace HappyFarmer.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServiceExtension(this IServiceCollection services)
        {
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
            services.AddScoped<IAboutUsRepository, EfCoreAboutUsRepository>();
            services.AddScoped<ISecurityInformationRepository, EfCoreSecurityInformationRepository>();
            services.AddScoped<ICartRepository, EfCoreCartRepository>();
            services.AddScoped<IOrderRepository, EfCoreOrderRepository>();

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
            services.AddScoped<IAboutUsService, AboutUsService>();
            services.AddScoped<ISecurityInformationService, SecurityInformationService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}
