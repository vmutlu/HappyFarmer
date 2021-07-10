using HappyFarmer.Business.Abstract;
using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.Business.Concrate
{
    public class BannerService : IBannerService
    {
        private readonly IBannerRepository _bannerRepository;
        public BannerService(IBannerRepository bannerRepository) =>
            _bannerRepository = bannerRepository;

        public void Create(FarmerBanner entity) =>
            _bannerRepository.Create(entity);

        public void Delete(FarmerBanner entity) =>
            _bannerRepository.Delete(entity);

        public List<FarmerBanner> GetAdminBanner() =>
             _bannerRepository.GetAdminBanner();

        public List<FarmerBanner> GetAll() =>
             _bannerRepository.GetAll();

        public FarmerBanner GetById(int id) =>
             _bannerRepository.GetById(id);

        public List<FarmerBanner> GetLowerAll() =>
             _bannerRepository.GetLowerAll();

        public void Update(FarmerBanner entity) =>
            _bannerRepository.Update(entity);
    }
}
