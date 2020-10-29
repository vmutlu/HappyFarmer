using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HappyFarmer.DataAccess.Concrete.EntityFrameWork
{
    public class EfCoreCategoryRepository : EfCoreGenericRepository<FarmerCategory, FarmerContext>, ICategoryRepository
    {
        public void DeleteFromCategory(int categoryId, int productId)
        {
            using (var context = new FarmerContext())
            {
                var sqlRequest = "Delete from farmerproductcategory where ProductId=@p0 and CategoryId=@p1";
                context.Database.ExecuteSqlCommand(sqlRequest, productId, categoryId);
            }
        }

        public FarmerCategory GetByIdWithProduct(int id)
        {
            using (var context = new FarmerContext())
            {
                return context.FarmerCategory
                    .Where(i => i.Id == id)
                    .Include(i => i.ProductCategories)
                    .ThenInclude(i => i.Product).AsNoTracking()
                    .FirstOrDefault();
            }

            //using (var context = new FarmerContext())
            //{
            //    var response = (from i in context.FarmerCategory
            //                    from a in i.ProductCategories
            //                    where i.Id == id
            //                    select new FarmerCategory()
            //                    {
            //                        Id = a.CategoryId,
            //                        Name = i.Name,
            //                        ProductCategories = new List<FarmerProductCategory>()
            //                         {
            //                          new FarmerProductCategory()
            //                          {
            //                              CategoryId = a.CategoryId,
            //                              Product = new FarmerProduct()
            //                              {
            //                                  Id = a.Product.Id,
            //                                  Name = a.Product.Name
            //                              },
            //                              ProductId = a.ProductId
            //                          }
            //                      }
            //                    }).FirstOrDefault();

            //    return response;
            //}
        }
    }
}
