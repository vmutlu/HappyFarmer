using HappyFarmer.Entities;

namespace HappyFarmer.DataAccess.Abstract
{
    public interface ICategoryRepository:IRepository<FarmerCategory>
    {
        FarmerCategory GetByIdWithProduct(int id);
        void DeleteFromCategory(int categoryId, int productId);
    }
}
