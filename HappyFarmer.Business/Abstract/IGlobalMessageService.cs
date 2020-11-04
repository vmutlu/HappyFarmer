using HappyFarmer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HappyFarmer.Business.Abstract
{
    public interface IGlobalMessageService
    {
        Task<List<FarmerGlobalMessage>> GetCarrierGlobalMessages(bool? authority = false);
        FarmerGlobalMessage GetById(int id);
        FarmerGlobalMessage GetNameById(int id);
        List<FarmerGlobalMessage> GetAll(bool? authority = false);
        void Create(FarmerGlobalMessage entity);
        void Delete(FarmerGlobalMessage entity);
        void Update(FarmerGlobalMessage entity);
        List<FarmerGlobalMessage> GetTypeGlobalMessages(string type);
        List<FarmerGlobalMessage> GetCityWithMessages(int cityId, string type);
    }
}
