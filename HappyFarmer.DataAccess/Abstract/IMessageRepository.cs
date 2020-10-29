using HappyFarmer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HappyFarmer.DataAccess.Abstract
{
    public interface IMessageRepository : IRepository<FarmerMessage>
    {
        List<FarmerMessage> GetBySenderId(int id);
        List<FarmerMessage> GetByReceiverId(int id);
    }
}
