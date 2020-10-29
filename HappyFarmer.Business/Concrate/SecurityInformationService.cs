using HappyFarmer.Business.Abstract;
using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.Business.Concrate
{
    public class SecurityInformationService : ISecurityInformationService
    {
        private readonly ISecurityInformationRepository _securityInformatinRepository;
        public SecurityInformationService(ISecurityInformationRepository securityInformatinRepository)
        {
            _securityInformatinRepository = securityInformatinRepository;
        }
        public void Create(FarmerSecurityInformation entity)
        {
            _securityInformatinRepository.Create(entity);
        }

        public void Delete(FarmerSecurityInformation entity)
        {
            _securityInformatinRepository.Delete(entity);
        }

        public List<FarmerSecurityInformation> GetAll()
        {
            return _securityInformatinRepository.GetAll();
        }

        public FarmerSecurityInformation GetById(int id)
        {
            return _securityInformatinRepository.GetById(id);
        }

        public void Update(FarmerSecurityInformation entity)
        {
            _securityInformatinRepository.Update(entity);
        }
    }
}
