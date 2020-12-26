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
        public void Create(FarmerProduct entity)
        {
            _productRepository.Create(entity);
        }

        public void Delete(FarmerProduct entity)
        {
            _productRepository.Delete(entity);
        }

        public List<FarmerProduct> FilterByPrice(int lowPrice, int topPrice, string type)
        {
            return _productRepository.FilterByPrice(lowPrice, topPrice, type);
        }

        public List<FarmerProduct> FindForProductsByCategoryType(string type, string searchText)
        {
            return _productRepository.FindForProductsByCategoryType(type, searchText);
        }

        public List<FarmerProduct> GetAll(int? page = 0, int? pageSize = 0)
        {
            using (var context = new FarmerContext())
            {
                if (page == 0 && pageSize == 0)
                {
                    return context.Products
                        .Where(i => i.PermissionToSell == true).Include(i => i.ProductCategories).Include(i=>i.ProductComments).Include(i=>i.MultipleProductImages)
                        .ToList();
                }

                else
                {
                    var products = context.Products.AsQueryable();

                    products = products
                        .Include(i => i.ProductCategories)
                        .Where(i => i.PermissionToSell == true);

                    return products.Skip((int)((page - 1) * pageSize)).Take((int)pageSize).ToList();
                }
            }

            #region Silinmişler
            //var getAll = (from i in context.Products
            //              from a in i.ProductCategories
            //              select new FarmerProduct()
            //              {
            //                  Id = a.ProductId,
            //                  Name = i.Name,
            //                  AnimalAge = i.AnimalAge,
            //                  City = i.City,
            //                  Country = i.Country,
            //                  AnnouncementDate = i.AnnouncementDate,
            //                  Description = i.Description,
            //                  FarmerDeclareType = i.FarmerDeclareType,
            //                  Guarantee = i.Guarantee,
            //                  ImageUrl = i.ImageUrl,
            //                  Neighborhood = i.Neighborhood,
            //                  PermissionToSell = i.PermissionToSell,
            //                  Situation = i.Situation,
            //                  Price = i.Price,
            //                  Swap = i.Swap,
            //                  UserId = i.UserId,
            //                  ProductCategories = new List<FarmerProductCategory>()
            //                  {
            //                      new FarmerProductCategory()
            //                      {
            //                          CategoryId = a.CategoryId,
            //                          Category = new FarmerCategory()
            //                          {
            //                              Id = a.Category.Id,
            //                              Name = a.Category.Name
            //                          },
            //                          ProductId = a.ProductId
            //                      }
            //                  }
            //              }).ToList();



            //using (var context = new FarmerContext())
            //{
            //    return context.Products
            //        .Where(i => i.PermissionToSell == true)
            //        .ToList();
            //}

            #endregion
        }

        public FarmerProduct GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public List<FarmerProduct> GetByIdUser(int userId)
        {
            return _productRepository.GetByIdUser(userId);
        }

        public FarmerProduct GetByIdWithCategories(int id)
        {
            return _productRepository.GetByIdWithCategories(id);
        }

        public int GetCountByCategory(string category)
        {
            return _productRepository.GetCountByCategory(category);
        }

        public List<FarmerProduct> GetCustomerDeclares(List<int> type)
        {
            return _productRepository.GetCustomerDeclares(type);
        }

        public List<FarmerProduct> GetPopularProduct(int? pageStandOut = 0, int? pageSize = 0)
        {
            return _productRepository.GetPopularProduct(pageStandOut, pageSize);
        }

        public List<FarmerProduct> GetProductByCategory(string category, int page, int pageSize)
        {
            return _productRepository.GetProductByCategory(category, page, pageSize);
        }

        public List<FarmerProduct> GetProductByType(string type, int userId)
        {
            return _productRepository.GetProductByType(type, userId);
        }

        public FarmerProduct GetProductDetails(int id)
        {
            return _productRepository.GetProductDetails(id);
        }

        public void Update(FarmerProduct entity, int[] categoryIds)
        {
            _productRepository.Update(entity, categoryIds);
        }

        public void Update(FarmerProduct entity)
        {
            _productRepository.Update(entity);
        }

        public void CreateComment(ProductComment productComment)
        {
            _productCommentRepository.Create(productComment);
        }

        public List<FarmerProduct> FilterByRegion(string type, string City, string Country, string Neighborhood)
        {
            return _productRepository.FilterByRegion(type, City, Country, Neighborhood);
        }

        public List<FarmerProduct> GetCategoryWithCount()
        {
            return _productRepository.GetCategoryWithCount();
        }

        public IEnumerable<string> GetCityProduct()
        {
            return _productRepository.GetCityProduct();
        }

        public List<FarmerProduct> GlobalFilter(string filteredByLessToMore)
        {
            return _productRepository.GlobalFilter(filteredByLessToMore);
        }
    }
}
