using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.Business.Abstract
{
    public interface ICityService
    {
        List<FarmerCities> GetAll();
        FarmerCities  GetById(int id);
    }
}
