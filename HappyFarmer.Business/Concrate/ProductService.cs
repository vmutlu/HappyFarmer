using HappyFarmer.Business.Abstract;
using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.DataAccess.Concrete;
using HappyFarmer.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace HappyFarmer.Business.Concrate
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCommentRepository _productCommentRepository;
        public ProductService(IProductRepository productRepository, IProductCommentRepository productCommentRepository)
        {
            _productRepository = productRepository;
            _productCommentRepository = productCommentRepository;
        }
        public void Create(FarmerProduct entity) =>
            _productRepository.Create(entity);

        public void Delete(FarmerProduct entity) =>
            _productRepository.Delete(entity);

        public List<FarmerProduct> FilterByPrice(int lowPrice, int topPrice, string type) =>
             _productRepository.FilterByPrice(lowPrice, topPrice, type);

        public List<FarmerProduct> FindForProductsByCategoryType(string type, string searchText) =>
             _productRepository.FindForProductsByCategoryType(type, searchText);

        public List<FarmerProduct> GetAll(int? page = 0, int? pageSize = 0)
        {
            using (var context = new FarmerContext())
            {
                if (page == 0 && pageSize == 0)
                    return context.Products
                        .Where(i => i.PermissionToSell == true).Include(i => i.ProductCategories).Include(i => i.ProductComments).Include(i => i.MultipleProductImages)
                        .ToList();

                else
                {
                    var products = context.Products.AsQueryable();

                    products = products
                        .Include(i => i.ProductCategories)
                        .Where(i => i.PermissionToSell == true);

                    return products.Skip((int)((page - 1) * pageSize)).Take((int)pageSize).ToList();
                }
            }
        }

        public FarmerProduct GetById(int id) =>
             _productRepository.GetById(id);

        public List<FarmerProduct> GetByIdUser(int userId) =>
             _productRepository.GetByIdUser(userId);

        public FarmerProduct GetByIdWithCategories(int id) =>
             _productRepository.GetByIdWithCategories(id);

        public int GetCountByCategory(string category) =>
             _productRepository.GetCountByCategory(category);

        public List<FarmerProduct> GetCustomerDeclares(List<int> type) =>
             _productRepository.GetCustomerDeclares(type);

        public List<FarmerProduct> GetPopularProduct(int? pageStandOut = 0, int? pageSize = 0) =>
             _productRepository.GetPopularProduct(pageStandOut, pageSize);

        public List<FarmerProduct> GetProductByCategory(string category, int page, int pageSize) =>
            _productRepository.GetProductByCategory(category, page, pageSize);

        public List<FarmerProduct> GetProductByType(string type, int userId) =>
             _productRepository.GetProductByType(type, userId);

        public FarmerProduct GetProductDetails(int id) =>
             _productRepository.GetProductDetails(id);

        public void Update(FarmerProduct entity, int[] categoryIds) =>
            _productRepository.Update(entity, categoryIds);

        public void Update(FarmerProduct entity) =>
            _productRepository.Update(entity);

        public void CreateComment(ProductComment productComment) =>
            _productCommentRepository.Create(productComment);

        public List<FarmerProduct> FilterByRegion(string type, string City, string Country, string Neighborhood) =>
             _productRepository.FilterByRegion(type, City, Country, Neighborhood);

        public List<FarmerProduct> GetCategoryWithCount() =>
             _productRepository.GetCategoryWithCount();

        public IEnumerable<string> GetCityProduct() =>
             _productRepository.GetCityProduct();

        public List<FarmerProduct> GlobalFilter(string filteredByLessToMore, string type) =>
             _productRepository.GlobalFilter(filteredByLessToMore, type);
    }
}
