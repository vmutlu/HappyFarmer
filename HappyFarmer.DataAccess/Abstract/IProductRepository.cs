using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.DataAccess.Abstract
{
    public interface IProductRepository : IRepository<FarmerProduct>
    {
        List<FarmerProduct> GetProductByCategory(string category, int page, int pageSize);
        FarmerProduct GetProductDetails(int id);
        int GetCountByCategory(string category);
        FarmerProduct GetByIdWithCategories(int id);
        List<FarmerProduct> GetByIdUser(int userId); //kullanıcının ilanları
        void Update(FarmerProduct entity, int[] categoryIds);
        void Update(FarmerProduct entity);
        List<FarmerProduct> GetProductByType(string type, int userId);
        List<FarmerProduct> FindForProductsByCategoryType(string type, string searchText);
        List<FarmerProduct> GetCustomerDeclares(List<int> type);
        List<FarmerProduct> GetPopularProduct(int? pageStandOut = 0, int? pageSize = 0);
        List<FarmerProduct> FilterByPrice(int lowPrice,int topPrice,string type);
        List<FarmerProduct> FilterByRegion(string type,string? City, string? Country, string? Neighborhood);
        List<FarmerProduct> GetCategoryWithCount();
        IEnumerable<string> GetCityProduct();
    }
}
