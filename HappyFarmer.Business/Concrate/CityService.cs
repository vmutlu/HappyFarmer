using HappyFarmer.Business.Abstract;
using HappyFarmer.DataAccess.DataAccess;
using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.Business.Concrate
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        public CityService(ICityRepository cityRepository) =>
            _cityRepository = cityRepository;

        public List<FarmerCities> GetAll() =>
             _cityRepository.GetAll();

        public FarmerCities GetById(int id) =>
             _cityRepository.GetById(id);
    }
}
