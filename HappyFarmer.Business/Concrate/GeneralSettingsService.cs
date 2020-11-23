using HappyFarmer.Business.Abstract;
using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.Business.Concrate
{
    public class GeneralSettingsService : IGeneralSettingsService
    {
        private readonly IGeneralSettingsRepository _generalSettingsRepository;
        public GeneralSettingsService(IGeneralSettingsRepository generalSettingsRepository)
        {
            _generalSettingsRepository = generalSettingsRepository;
        }
        public void Create(FarmerGeneralSettings entity)
        {
            _generalSettingsRepository.Create(entity);
        }

        public void Delete(FarmerGeneralSettings entity)
        {
            _generalSettingsRepository.Delete(entity); 
        }

        public List<FarmerGeneralSettings> GetAll()
        {
            return _generalSettingsRepository.GetAll();
        }

        public FarmerGeneralSettings GetById(int id)
        {
            return _generalSettingsRepository.GetById(id);
        }

        public void Update(FarmerGeneralSettings entity)
        {
            _generalSettingsRepository.Update(entity);
        }
    }
}
