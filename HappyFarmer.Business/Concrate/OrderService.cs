﻿using HappyFarmer.Business.Abstract;
using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.Business.Concrate
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository) =>
            _orderRepository = orderRepository;

        public void Create(FarmerOrder entity) =>
            _orderRepository.Create(entity);

        public FarmerOrder GetById(string orderId) =>
             _orderRepository.GetById(orderId);

        public List<FarmerOrder> GetOrders(string userId) =>
             _orderRepository.GetOrders(userId);

        public int GetPopularProduct() =>
             _orderRepository.GetPopularProduct();
    }
}
