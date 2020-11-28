using HappyFarmer.DataAccess.DataAccess;
using HappyFarmer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace HappyFarmer.DataAccess.Concrete.EntityFrameWork
{
    public class EfCoreCityRepository : EfCoreGenericRepository<FarmerCities, FarmerContext>, ICityRepository
    {
        public override List<FarmerCities> GetAll(Expression<Func<FarmerCities, bool>> filter = null, bool? authority = false)
        {
            using (var context = new FarmerContext())
            {
                var response = context.Cities.AsNoTracking().ToList();

                return response;
            }
        }

        public override FarmerCities GetById(int id)
        {
            using (var context = new FarmerContext())
            {
                var response = context.Cities
                    .AsNoTracking().
                    FirstOrDefault(i => i.Id == id);

                return response;
            }
        }
    }
}
