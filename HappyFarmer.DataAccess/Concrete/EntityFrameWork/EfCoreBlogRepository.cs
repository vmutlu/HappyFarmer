using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace HappyFarmer.DataAccess.Concrete.EntityFrameWork
{
    public class EfCoreBlogRepository : EfCoreGenericRepository<FarmerBlog, FarmerContext>, IBlogRepository
    {
    }
}
