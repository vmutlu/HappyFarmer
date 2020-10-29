using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.Business.Abstract
{
    public interface ISliderService
    {
        FarmerSlider GetById(int id);
        List<FarmerSlider> GetAll(bool? authority = false);
        void Create(FarmerSlider entity);
        void Delete(FarmerSlider entity);
        void Update(FarmerSlider entity);
    }
}
