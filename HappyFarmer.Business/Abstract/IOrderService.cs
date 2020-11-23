using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.Business.Abstract
{
    public interface IOrderService
    {
        void Create(FarmerOrder entity);
        List<FarmerOrder> GetOrders(string userId);
        int GetPopularProduct();
        FarmerOrder GetById(string orderId);
    }
}
