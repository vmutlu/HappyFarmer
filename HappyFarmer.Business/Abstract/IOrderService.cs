using HappyFarmer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
