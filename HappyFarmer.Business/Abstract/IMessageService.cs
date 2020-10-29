using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.Business.Abstract
{
    public interface IMessageService : IBaseService<FarmerMessage>
    {
        List<FarmerMessage> GetBySenderId(int id);
        List<FarmerMessage> GetByReceiverId(int id);
    }
}
