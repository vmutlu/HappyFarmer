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
        }
    }
}
