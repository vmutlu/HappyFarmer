using HappyFarmer.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyFarmer.Business.Abstract
{
    public interface IProductService
    {
        FarmerProduct GetById(int id);
        FarmerProduct GetProductDetails(int id);
        List<FarmerProduct> GetAll(int? page = 0, int? pageSize = 0);
        List<FarmerProduct> GetProductByCategory(string category, int page, int pageSize);
        void Create(FarmerProduct entity);
        void Delete(FarmerProduct entity);
        int GetCountByCategory(string category);
        FarmerProduct GetByIdWithCategories(int id);
        void Update(FarmerProduct entity, int[] categoryIds);
        List<FarmerProduct> GetProductByType(string type, int userId);
        List<FarmerProduct> FindForProductsByCategoryType(string type, string searchText);
        List<FarmerProduct> GetByIdUser(int userId);
        List<FarmerProduct> GetCustomerDeclares(List<int> type);
        void Update(FarmerProduct entity);
        List<FarmerProduct> GetPopularProduct(int? pageStandOut = 0, int? pageSize = 0);
        List<FarmerProduct> FilterByPrice(int lowPrice, int topPrice, string type);
        void CreateComment(ProductComment productComment);
        List<FarmerProduct> FilterByRegion(string type, string? City, string? Country, string? Neighborhood);
        List<FarmerProduct> GetCategoryWithCount();
    }
}
