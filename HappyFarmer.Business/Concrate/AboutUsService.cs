using HappyFarmer.Business.Abstract;
using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HappyFarmer.Business.Concrate
{
    public class AboutUsService : IAboutUsService
    {
        private readonly IAboutUsRepository _aboutUsRepository;

        public AboutUsService(IAboutUsRepository aboutUsRepository)
        {
            _aboutUsRepository = aboutUsRepository;
        }
        public void Create(FarmerAboutUs entity)
        {
            _aboutUsRepository.Create(entity);
        }

        public void Delete(FarmerAboutUs entity)
        {
            _aboutUsRepository.Delete(entity);
        }

        public List<FarmerAboutUs> GetAll()
        {
            return _aboutUsRepository.GetAll();
        }

        public FarmerAboutUs GetById(int id)
        {
            return _aboutUsRepository.GetById(id);
        }

        public void Update(FarmerAboutUs entity)
        {
            _aboutUsRepository.Update(entity);
        }
    }
}
