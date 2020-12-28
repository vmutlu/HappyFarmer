using AutoMapper;
using HappyFarmer.API.DTOs;
using HappyFarmer.Entities;

namespace HappyFarmer.API.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<FarmerCategory, FarmerCategoryDTO>();
            CreateMap<FarmerCategoryDTO, FarmerCategory>();

            CreateMap<FarmerProductDTO, FarmerProduct>();
            CreateMap<FarmerProduct, FarmerProductDTO>();

            CreateMap<FarmerProductCategoryDTO, FarmerProduct>();
            CreateMap<FarmerProduct, FarmerProductCategoryDTO>();

            CreateMap<FarmerCategory, FarmerProductCategoryDTO>();
            CreateMap<FarmerProductCategoryDTO, FarmerCategory>();

            CreateMap<CategoryWithProductDTO, FarmerCategory>();
            CreateMap<FarmerCategory, CategoryWithProductDTO>();

            CreateMap<CategoryWithProductDTO, FarmerProduct>();
            CreateMap<FarmerProduct, CategoryWithProductDTO>();

            CreateMap<FarmerAdminMessage, FarmerAdminMessageDTO>();
            CreateMap<FarmerAdminMessageDTO, FarmerAdminMessage>();

            CreateMap<FarmerBanner, FarmerBannerDTO>();
            CreateMap<FarmerBannerDTO, FarmerBanner>();

            CreateMap<FarmerSlider, FarmerSliderDTO>();
            CreateMap<FarmerSliderDTO, FarmerSlider>();

            CreateMap<FarmerUserDTO, FarmerUser>();
            CreateMap<FarmerUser, FarmerUserDTO>();

            CreateMap<FarmerAboutUsDTO, FarmerAboutUs>();
            CreateMap<FarmerAboutUs, FarmerAboutUsDTO>();

            CreateMap<FarmerSecurityInformationDTO, FarmerSecurityInformation>();
            CreateMap<FarmerSecurityInformation, FarmerSecurityInformationDTO>();

            CreateMap<FarmerAdminMessage, FarmerAdminMessageDTO>();
            CreateMap<FarmerAdminMessageDTO, FarmerAdminMessage>();

            CreateMap<FarmerCartDTO, FarmerCart>();
            CreateMap<FarmerCart, FarmerCartDTO>();
        }
    }
}
