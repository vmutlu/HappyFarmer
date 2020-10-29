using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HappyFarmer.DataAccess.Concrete.EntityFrameWork
{
    public class EfCoreMultipleProductImagesRepository : EfCoreGenericRepository<FarmerMultipleProductImages, FarmerContext>, IMultipleProductImagesRepository
    {
        public void Create(int productId, List<string> multipleImages)
        {
            using (var context = new FarmerContext())
            {
                foreach (var item in multipleImages)
                {
                    var sqlRequest = "INSERT INTO multipleproductimages (ProductId,ImageURL) VALUES(@p0,@p1)";
                    context.Database.ExecuteSqlCommand(sqlRequest, productId, item);
                }
            }
        }

        public List<FarmerMultipleProductImages> GetByIdMultiImages(int id)
        {
            using (var context = new FarmerContext())
            {
                var response = context.MultipleProductImages
                    .Where(i => i.ProductId == id).AsNoTracking().ToList();

                return response;
            }
        }
    }
}
