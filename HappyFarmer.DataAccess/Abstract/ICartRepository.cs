using HappyFarmer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HappyFarmer.DataAccess.Abstract
{
    public interface ICartRepository:IRepository<FarmerCart>
    {
        FarmerCart GetByUserId(string userId);
        void DeleteFromCard(int cardId, int productId);
        void ClearCart(string cartId);
    }
}
