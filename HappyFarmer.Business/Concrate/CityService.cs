using HappyFarmer.Business.Abstract;
using HappyFarmer.DataAccess.DataAccess;
using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.Business.Concrate
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        public List<FarmerCities> GetAll()
        {
            return _cityRepository.GetAll();
        }

        public FarmerCities GetById(int id)
        {
            return _cityRepository.GetById(id);
        }
    }
}
