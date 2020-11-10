using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace HappyFarmer.DataAccess.Concrete.EntityFrameWork
{
    public class EfCoreOrderRepository : EfCoreGenericRepository<FarmerOrder, FarmerContext>, IOrderRepository
    {
        public FarmerOrder GetById(string orderId)
        {
            using (var context = new FarmerContext())
            {
                var response = context.Orders
                    .FirstOrDefault(i => i.Id == Convert.ToInt32(orderId));

                return response;
            }
        }

        public List<FarmerOrder> GetOrders(string userId)
        {
            using (var context = new FarmerContext())
            {
                var orders = context.Orders
                    .Include(i => i.FarmerOrderItems)
                    .ThenInclude(i => i.FarmerProduct).AsNoTracking()
                    .AsQueryable();

                if (!string.IsNullOrEmpty(userId))
                {
                    orders = orders.Where(i => i.UserId == Convert.ToInt32(userId));
                }

                return orders.ToList();
            }
        }

        //sipariş tablosunda en çok satılan ürünün Idsini ver
        public int GetPopularProduct()
        {
            using (var context = new FarmerContext())
            {
                var response = context.OrderItems
                    .OrderByDescending(i => i.Quantity).FirstOrDefault();

                return response.ProductId;
            }
        }
    }
}
