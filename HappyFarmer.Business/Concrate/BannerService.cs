using HappyFarmer.Business.Abstract;
using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.Business.Concrate
{
    public class BannerService : IBannerService
    {
        private readonly IBannerRepository _bannerRepository;
        public BannerService(IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }
        public void Create(FarmerBanner entity)
        {
            _bannerRepository.Create(entity);
        }

        public void Delete(FarmerBanner entity)
        {
            _bannerRepository.Delete(entity);
        }

        public List<FarmerBanner> GetAdminBanner()
        {
            return _bannerRepository.GetAdminBanner();
        }

        public List<FarmerBanner> GetAll()
        {
            return _bannerRepository.GetAll();
        }

        public FarmerBanner GetById(int id)
        {
            return _bannerRepository.GetById(id);
        }

        public List<FarmerBanner> GetLowerAll()
        {
            return _bannerRepository.GetLowerAll();
        }

        public void Update(FarmerBanner entity)
        {
            _bannerRepository.Update(entity);
        }
    }
}
