using HappyFarmer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HappyFarmer.DataAccess.Abstract
{
    public interface IGlobalMessageRepository : IRepository<FarmerGlobalMessage>
    {
        FarmerGlobalMessage GetNameById(int id);
        Task<List<FarmerGlobalMessage>> GetCarrierGlobalMessages(bool? authority = false);
        List<FarmerGlobalMessage> GetTypeGlobalMessages(string type);
        List<FarmerGlobalMessage> GetCityWithMessages(int cityId, string type);
    }
}
