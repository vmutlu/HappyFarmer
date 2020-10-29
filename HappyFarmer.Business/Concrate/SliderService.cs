using HappyFarmer.Business.Abstract;
using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.DataAccess.Concrete;
using HappyFarmer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappyFarmer.Business.Concrate
{
    public class SliderService : ISliderService
    {
        private ISliderRepository _sliderRepository;
        public SliderService(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }
        public void Create(FarmerSlider entity)
        {
            _sliderRepository.Create(entity);
        }

        public void Delete(FarmerSlider entity)
        {
            _sliderRepository.Delete(entity);
        }

        public List<FarmerSlider> GetAll(bool? authority = false)
        {
            return _sliderRepository.GetAll(null, authority);
        }

        public FarmerSlider GetById(int id)
        {
            return _sliderRepository.GetById(id);
        }

        public void Update(FarmerSlider entity)
        {
            _sliderRepository.Update(entity);
        }
    }
}
