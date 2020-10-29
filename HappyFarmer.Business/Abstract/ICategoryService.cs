using HappyFarmer.Entities;

namespace HappyFarmer.Business.Abstract
{
    public interface ICategoryService : IBaseService<FarmerCategory>
    {
        FarmerCategory GetByIdWithProducts(int id);
        void DeleteFromCategory(int categoryId, int productId);
    }
}
