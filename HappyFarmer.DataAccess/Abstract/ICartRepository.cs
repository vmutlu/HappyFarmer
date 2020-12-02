using HappyFarmer.Entities;

namespace HappyFarmer.DataAccess.Abstract
{
    public interface ICartRepository:IRepository<FarmerCart>
    {
        FarmerCart GetByUserId(string userId);
        void DeleteFromCard(int cardId, int productId);
        void ClearCart(string cartId);
    }
}
