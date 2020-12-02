using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.DataAccess.Abstract
{
    public interface IMessageRepository : IRepository<FarmerMessage>
    {
        List<FarmerMessage> GetBySenderId(int id);
        List<FarmerMessage> GetByReceiverId(int id);
    }
}
