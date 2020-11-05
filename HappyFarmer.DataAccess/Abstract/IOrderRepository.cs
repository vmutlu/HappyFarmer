using HappyFarmer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HappyFarmer.DataAccess.Abstract
{
    public interface IOrderRepository : IRepository<FarmerOrder>
    {
        List<FarmerOrder> GetOrders(string userId);
        FarmerOrder GetById(string orderId);
        int GetPopularProduct();
    }
}
