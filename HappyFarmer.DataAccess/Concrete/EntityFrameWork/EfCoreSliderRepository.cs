using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace HappyFarmer.DataAccess.Concrete.EntityFrameWork
{
    public class EfCoreSliderRepository : EfCoreGenericRepository<FarmerSlider, FarmerContext>, ISliderRepository
    {
        public override List<FarmerSlider> GetAll(Expression<Func<FarmerSlider, bool>> filter = null, bool? authority = false)
        {
            using (var context = new FarmerContext())
            {
                if(authority == true)
                {
                    var response = context.Sliders.AsNoTracking().ToList();

                    return response;
                }

                else
                {
                    var response = context.Sliders
                        .Where(i => i.Situation == true)
                        .OrderBy(i => i.SequenceNumber).AsNoTracking().ToList();

                    return response;
                }
            }
        }
    }
}
