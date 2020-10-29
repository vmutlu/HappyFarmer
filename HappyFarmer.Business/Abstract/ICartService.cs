using HappyFarmer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HappyFarmer.Business.Abstract
{
   public interface ICartService
    {
        void InitializeCart(string userId);
        FarmerCart GetCartByUserId(string userId);
        void AddToCart(string userId, int productId, int quantity);
        void DeleteFromCart(string userId, int productId);
        void ClearCart(string cartId);
    }
}
