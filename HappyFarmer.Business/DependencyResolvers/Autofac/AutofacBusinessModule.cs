using Autofac;
using HappyFarmer.Business.Abstract;
using HappyFarmer.Business.Concrate;
using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.DataAccess.Concrete.EntityFrameWork;
using HappyFarmer.DataAccess.DataAccess;

namespace HappyFarmer.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductService>().As<IProductService>().SingleInstance();
            builder.RegisterType<EfCoreProductRepository>().As<IProductRepository>().SingleInstance();

            builder.RegisterType<CategoryService>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<EfCoreCategoryRepository>().As<ICategoryRepository>().SingleInstance();

            builder.RegisterType<UserService>().As<IUserService>().SingleInstance();
            builder.RegisterType<EfCoreUserRepository>().As<IUserRepository>().SingleInstance();

            builder.RegisterType<SliderService>().As<ISliderService>().SingleInstance();
            builder.RegisterType<EfCoreSliderRepository>().As<ISliderRepository>().SingleInstance();

            builder.RegisterType<AboutUsService>().As<IAboutUsService>().SingleInstance();
            builder.RegisterType<EfCoreAboutUsRepository>().As<IAboutUsRepository>().SingleInstance();
             
            builder.RegisterType<AdminMessageService>().As<IAdminMessageService>().SingleInstance();
            builder.RegisterType<EfCoreAdminMessageRepository>().As<IAdminMessageRepository>().SingleInstance();

            builder.RegisterType<GeneralSettingsService>().As<IGeneralSettingsService>().SingleInstance();
            builder.RegisterType<EfCoreGeneralSettingsRepository>().As<IGeneralSettingsRepository>().SingleInstance();

            builder.RegisterType<MessageService>().As<IMessageService>().SingleInstance();
            builder.RegisterType<EfCoreMessageRepository>().As<IMessageRepository>().SingleInstance();

            builder.RegisterType<MultipleProductImagesService>().As<IMultipleProductImagesService>().SingleInstance();
            builder.RegisterType<EfCoreMultipleProductImagesRepository>().As<IMultipleProductImagesRepository>().SingleInstance();

            builder.RegisterType<GlobalMessageService>().As<IGlobalMessageService>().SingleInstance();
            builder.RegisterType<EfCoreGlobalMessageRepository>().As<IGlobalMessageRepository>().SingleInstance();

            builder.RegisterType<BlogService>().As<IBlogService>().SingleInstance();
            builder.RegisterType<EfCoreBlogRepository>().As<IBlogRepository>().SingleInstance();

            builder.RegisterType<CartService>().As<ICartService>().SingleInstance();
            builder.RegisterType<EfCoreCartRepository>().As<ICartRepository>().SingleInstance();

            builder.RegisterType<SecurityInformationService>().As<ISecurityInformationService>().SingleInstance();
            builder.RegisterType<EfCoreSecurityInformationRepository>().As<ISecurityInformationRepository>().SingleInstance();

            builder.RegisterType<OrderService>().As<IOrderService>().SingleInstance();
            builder.RegisterType<EfCoreOrderRepository>().As<IOrderRepository>().SingleInstance();

            builder.RegisterType<EfCoreCommentRepository>().As<ICommentRepository>().SingleInstance();

            builder.RegisterType<BannerService>().As<IBannerService>().SingleInstance();
            builder.RegisterType<EfCoreBannerRepository>().As<IBannerRepository>().SingleInstance();

            builder.RegisterType<EfCoreProductCommentRepository>().As<IProductCommentRepository>().SingleInstance();

            builder.RegisterType<CityService>().As<ICityService>().SingleInstance();
            builder.RegisterType<EfCoreCityRepository>().As<ICityRepository>().SingleInstance();
        }
    }
}
