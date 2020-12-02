using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.DataAccess.Abstract
{
    public interface IOrderRepository : IRepository<FarmerOrder>
    {
        List<FarmerOrder> GetOrders(string userId);
        FarmerOrder GetById(string orderId);
        int GetPopularProduct();
    }
}
