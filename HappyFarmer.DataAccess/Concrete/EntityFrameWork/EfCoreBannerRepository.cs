using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace HappyFarmer.DataAccess.Concrete.EntityFrameWork
{
    public class EfCoreBannerRepository : EfCoreGenericRepository<FarmerBanner, FarmerContext>, IBannerRepository
    {
        public List<FarmerBanner> GetAdminBanner()
        {
            using (var context = new FarmerContext())
            {
                var response = context.Banners
                    .AsNoTracking()
                    .ToList();

                return response;
            }
        }

        public override List<FarmerBanner> GetAll(Expression<Func<FarmerBanner, bool>> filter = null, bool? authority = false)
        {
            using (var context = new FarmerContext())
            {
                var response = context.Banners
                    .Where(i => i.LowerUpper == true)
                    .Where(i=>i.Active == true)
                    .AsNoTracking()
                    .ToList();

                return response;
            }
        }
        public List<FarmerBanner> GetLowerAll()
        {
            using (var context = new FarmerContext())
            {
                var response = context.Banners
                    .Where(i => i.LowerUpper == false)
                    .Where(i => i.Active == true)
                    .AsNoTracking()
                    .ToList();

                return response;
            }
        }
    }
}
