using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.DataAccess.Enums;
using HappyFarmer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HappyFarmer.DataAccess.Concrete.EntityFrameWork
{
    public class EfCoreProductRepository : EfCoreGenericRepository<FarmerProduct, FarmerContext>, IProductRepository
    {
        public List<FarmerProduct> FilterByPrice(int lowPrice, int topPrice, string type)
        {

            #region Animals Enum
            var productTypeEnumIndex = 0;

            if (type == ProductAnimalEnum.BuyukBas.ToString())
                productTypeEnumIndex = 10;

            else if (type == ProductAnimalEnum.KucukBas.ToString())
                productTypeEnumIndex = 11;

            else if (type == ProductAnimalEnum.Kumes.ToString())
                productTypeEnumIndex = 12;

            else if (type == ProductAnimalEnum.Evcil.ToString())
                productTypeEnumIndex = 13;

            else if (type == ProductAnimalEnum.Sut.ToString())
                productTypeEnumIndex = 14;

            else if (type == ProductAnimalEnum.YiyecekIcecek.ToString())
                productTypeEnumIndex = 15;
            #endregion

            #region Agriculture Enum
            if (type == ProductAgricultureEnum.Traktor.ToString())
                productTypeEnumIndex = 20;

            else if (type == ProductAgricultureEnum.Tarla.ToString())
                productTypeEnumIndex = 21;

            else if (type == ProductAgricultureEnum.Ekipman.ToString())
                productTypeEnumIndex = 22;

            else if (type == ProductAgricultureEnum.Sebze.ToString())
                productTypeEnumIndex = 23;

            else if (type == ProductAgricultureEnum.Meyve.ToString())
                productTypeEnumIndex = 24;

            else if (type == ProductAgricultureEnum.Tahıl.ToString())
                productTypeEnumIndex = 25;

            else if (type == ProductAgricultureEnum.YemAndOtsuBitki.ToString())
                productTypeEnumIndex = 26;
            #endregion

            using (var context = new FarmerContext())
            {
                var response = context.Products
                    .Where(i => i.FarmerDeclareType == productTypeEnumIndex)
                    .Where(i => i.Price >= lowPrice)
                    .Where(i => i.Price <= topPrice)
                    .Where(i => i.PermissionToSell == true)
                    .AsNoTracking()
                    .ToList();

                return response;
            }
        }

        public List<FarmerProduct> FindForProductsByCategoryType(string type, string searchText)
        {
            using (var context = new FarmerContext())
            {
                var response = context.Products
                    .Where(i => i.FarmerDeclareType == Convert.ToInt32(type))
                    .Where(i => i.Name.Contains(searchText)).ToList();

                return response;
            }
        }

        public List<FarmerProduct> GetByIdUser(int userId)
        {
            using (var context = new FarmerContext())
            {
                var response = context.Products
                    .Where(i => i.UserId == userId).AsNoTracking()
                    .ToList();

                return response;
            }
        }

        public FarmerProduct GetByIdWithCategories(int id)
        {
            using (var context = new FarmerContext())
            {
                return context.Products
                    .Where(i => i.Id == id)
                    .Include(i => i.ProductCategories)
                    .ThenInclude(i => i.Category).AsNoTracking()
                    .FirstOrDefault();
            }

            //using (var context = new FarmerContext())
            //{
            //    var response = (from i in context.Products
            //                   from a in i.ProductCategories
            //                   where i.Id == id
            //                   select new FarmerProduct()
            //                   {
            //                       Id = a.ProductId,
            //                       Name = i.Name,
            //                       AnimalAge = i.AnimalAge,
            //                       City = i.City,
            //                       Country = i.Country,
            //                       AnnouncementDate = i.AnnouncementDate,
            //                       Description = i.Description,
            //                       FarmerDeclareType = i.FarmerDeclareType,
            //                       Guarantee = i.Guarantee,
            //                       ImageUrl = i.ImageUrl,
            //                       Neighborhood = i.Neighborhood,
            //                       PermissionToSell = i.PermissionToSell,
            //                       Situation = i.Situation,
            //                       Price = i.Price,
            //                       Swap = i.Swap,
            //                       UserId = i.UserId,
            //                       ProductCategories = new List<FarmerProductCategory>()
            //                      {
            //                          new FarmerProductCategory()
            //                          {
            //                              CategoryId = a.CategoryId,
            //                              Category = new FarmerCategory()
            //                              {
            //                                  Id = a.Category.Id,
            //                                  Name = a.Category.Name
            //                              },
            //                              ProductId = a.ProductId
            //                          }
            //                      }
            //                   }).FirstOrDefault();

            //    return response;
            //}          
        }
        //public List<FarmerProduct> GetAll()
        //{
        //    using (var context = new FarmerContext())
        //    {
        //        var response = (from i in context.Products
        //                        from a in i.ProductCategories
        //                        select new FarmerProduct
        //                        {
        //                                Id = i.Id,
        //                                Name = i.Name
        //                        }).ToList();
        //        return response;
        //    }
        //}
        public int GetCountByCategory(string category)
        {
            using (var context = new FarmerContext())
            {
                var products = context.Products.AsQueryable();

                if (!string.IsNullOrEmpty(category))
                {
                    products = products
                        .Include(i => i.ProductCategories)
                        .ThenInclude(i => i.Category).AsNoTracking()
                        .Where(i => i.ProductCategories.Any(i => i.Category.Name.ToLower() == category.ToLower()));
                }
                return products.Count();
            }
        }

        //müşteri ilanlarını getir
        public List<FarmerProduct> GetCustomerDeclares(List<int> type)
        {
            using (var context = new FarmerContext())
            {
                var response = context.Products
                    .Where(i => i.UserId != 1)
                    .OrderByDescending(i => i.AnnouncementDate).AsNoTracking().ToList();

                return response;
            }
        }

        public List<FarmerProduct> GetPopularProduct()
        {
            using (var context = new FarmerContext())
            {
                var response = context.Products.AsNoTracking()
                     .Where(i => i.Situation == "EVET").ToList();

                return response;
            }
        }

        public List<FarmerProduct> GetProductByCategory(string category, int page, int pageSize)
        {
            using (var context = new FarmerContext())
            {
                var products = context.Products.AsQueryable();

                if (!string.IsNullOrEmpty(category))
                {
                    products = products
                        .Include(i => i.ProductCategories)
                        .ThenInclude(i => i.Category).AsNoTracking()
                        .Where(i => i.ProductCategories.Any(i => i.Category.Name.ToLower() == category.ToLower()));
                }

                return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        //admin panelinde her ilanı ayrı ayrı getir
        public List<FarmerProduct> GetProductByType(string type, int userId)
        {
            #region Animals Enum
            var productTypeEnumIndex = 0;

            if (type == ProductAnimalEnum.BuyukBas.ToString())
                productTypeEnumIndex = 10;

            else if (type == ProductAnimalEnum.KucukBas.ToString())
                productTypeEnumIndex = 11;

            else if (type == ProductAnimalEnum.Kumes.ToString())
                productTypeEnumIndex = 12;

            else if (type == ProductAnimalEnum.Evcil.ToString())
                productTypeEnumIndex = 13;

            else if (type == ProductAnimalEnum.Sut.ToString())
                productTypeEnumIndex = 14;

            else if (type == ProductAnimalEnum.YiyecekIcecek.ToString())
                productTypeEnumIndex = 15;
            #endregion

            #region Agriculture Enum
            if (type == ProductAgricultureEnum.Traktor.ToString())
                productTypeEnumIndex = 20;

            else if (type == ProductAgricultureEnum.Tarla.ToString())
                productTypeEnumIndex = 21;

            else if (type == ProductAgricultureEnum.Ekipman.ToString())
                productTypeEnumIndex = 22;

            else if (type == ProductAgricultureEnum.Sebze.ToString())
                productTypeEnumIndex = 23;

            else if (type == ProductAgricultureEnum.Meyve.ToString())
                productTypeEnumIndex = 24;

            else if (type == ProductAgricultureEnum.Tahıl.ToString())
                productTypeEnumIndex = 25;

            else if (type == ProductAgricultureEnum.YemAndOtsuBitki.ToString())
                productTypeEnumIndex = 26;
            #endregion

            using (var context = new FarmerContext())
            {
                if (userId == 1)
                {
                    var productType = context.Products
                   .Where(i => i.FarmerDeclareType == productTypeEnumIndex)
                   .AsNoTracking()
                   .ToList();

                    return productType;
                }
                else
                {
                    var productType = context.Products
                   .Where(i => i.FarmerDeclareType == productTypeEnumIndex)
                   .Where(i => i.PermissionToSell == true)
                   .AsNoTracking()
                   .ToList();

                    return productType;
                }
            }
        }

        public FarmerProduct GetProductDetails(int id)
        {
            using (var context = new FarmerContext())
            {
                return context.Products
                    .Where(i => i.Id == id)
                    .Include(i => i.ProductCategories)
                    .ThenInclude(i => i.Category)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public void Update(FarmerProduct entity, int[] categoryIds)
        {
            using (var context = new FarmerContext())
            {
                var products = context.Products
                    .Include(i => i.ProductCategories)
                    .FirstOrDefault(i => i.Id == entity.Id);

                if (products != null)
                {
                    products.Name = entity.Name;
                    products.Description = entity.Description;
                    products.ImageUrl = entity.ImageUrl;
                    products.Neighborhood = entity.Neighborhood;
                    products.Price = entity.Price;
                    products.Situation = entity.Situation;
                    products.Swap = entity.Swap;
                    products.Guarantee = entity.Guarantee;
                    products.FarmerDeclareType = entity.FarmerDeclareType;
                    products.City = entity.City;
                    products.Country = entity.Country;
                    products.AnnouncementDate = entity.AnnouncementDate;
                    products.PermissionToSell = entity.PermissionToSell;
                    products.AnimalAge = entity.AnimalAge;

                    products.ProductCategories = categoryIds.Select(i => new FarmerProductCategory()
                    {
                        CategoryId = i,
                        ProductId = entity.Id
                    }).ToList();

                    context.SaveChanges();
                }
            }
        }

        #region Product Details And Product Comments

        public override FarmerProduct GetById(int id)
        {
            using (var context = new FarmerContext())
            {
                var response = (from i in context.Products
                                where i.Id == id
                                select new FarmerProduct
                                {
                                    AnimalAge = i.AnimalAge,
                                    AnnouncementDate = i.AnnouncementDate,
                                    City = i.City,
                                    Country = i.Country,
                                    Description = i.Description,
                                    FarmerDeclareType = i.FarmerDeclareType,
                                    Gender = i.Gender,
                                    Guarantee = i.Guarantee,
                                    Id = i.Id,
                                    ImageUrl = i.ImageUrl,
                                    Name = i.Name,
                                    Neighborhood = i.Neighborhood,
                                    PermissionToSell = i.PermissionToSell,
                                    Price = i.Price,
                                    Situation = i.Situation,
                                    StockQty = i.StockQty,
                                    Swap = i.Swap,
                                    Title = i.Title,
                                    UserEMail = i.UserEMail,
                                    UserId = i.UserId,
                                    UserName = i.UserName,
                                    UserPhoneNumber = i.UserPhoneNumber,
                                    UserSurname = i.UserSurname,
                                    ProductComments = (from c in i.ProductComments
                                                       where i.Id == c.ProductId
                                                       select new ProductComment
                                                       {
                                                           ProductId = c.ProductId,
                                                           Email = c.Email,
                                                           CommentDate = c.CommentDate,
                                                           Id = c.Id,
                                                           Comment = c.Comment,
                                                           Name = c.Name,
                                                           Surname = c.Surname,
                                                           UserId = c.UserId,
                                                           WebSite = c.WebSite
                                                       }).ToList()
                                }).FirstOrDefault();

                return response;
            }
        }

        public List<FarmerProduct> FilterByRegion(string type,string? City, string? Country, string? Neighborhood)
        {
            #region Animals Enum
            var productTypeEnumIndex = 0;

            if (type == ProductAnimalEnum.BuyukBas.ToString())
                productTypeEnumIndex = 10;

            else if (type == ProductAnimalEnum.KucukBas.ToString())
                productTypeEnumIndex = 11;

            else if (type == ProductAnimalEnum.Kumes.ToString())
                productTypeEnumIndex = 12;

            else if (type == ProductAnimalEnum.Evcil.ToString())
                productTypeEnumIndex = 13;

            else if (type == ProductAnimalEnum.Sut.ToString())
                productTypeEnumIndex = 14;

            else if (type == ProductAnimalEnum.YiyecekIcecek.ToString())
                productTypeEnumIndex = 15;
            #endregion

            #region Agriculture Enum
            if (type == ProductAgricultureEnum.Traktor.ToString())
                productTypeEnumIndex = 20;

            else if (type == ProductAgricultureEnum.Tarla.ToString())
                productTypeEnumIndex = 21;

            else if (type == ProductAgricultureEnum.Ekipman.ToString())
                productTypeEnumIndex = 22;

            else if (type == ProductAgricultureEnum.Sebze.ToString())
                productTypeEnumIndex = 23;

            else if (type == ProductAgricultureEnum.Meyve.ToString())
                productTypeEnumIndex = 24;

            else if (type == ProductAgricultureEnum.Tahıl.ToString())
                productTypeEnumIndex = 25;

            else if (type == ProductAgricultureEnum.YemAndOtsuBitki.ToString())
                productTypeEnumIndex = 26;
            #endregion

            using (var context = new FarmerContext())
            {
                if (City != null && Country != null && Neighborhood != null)
                {
                    var response = context.Products
                        .Where(i=>i.FarmerDeclareType == productTypeEnumIndex)
                        .Where(i => i.City == City)
                        .Where(i => i.Country == Country)
                        .Where(i => i.Neighborhood == Neighborhood)
                        .AsNoTracking()
                        .ToList();

                    return response;
                }

                else if(City!=null && Country != null)
                {
                    var response = context.Products
                        .Where(i => i.FarmerDeclareType == productTypeEnumIndex)
                        .Where(i => i.City == City)
                        .Where(i => i.Country == Country)
                        .AsNoTracking()
                        .ToList();

                    return response;
                }

                else
                {
                    var response = context.Products
                       .Where(i => i.FarmerDeclareType == productTypeEnumIndex)
                       .Where(i => i.City == City)
                       .AsNoTracking()
                       .ToList();

                    return response;
                }
            }
        }

        #endregion
    }
}
