using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;
using Microsoft.EntityFrameworkCore;
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
        }
    }
}
