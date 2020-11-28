﻿using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;

namespace HappyFarmer.DataAccess.Concrete.EntityFrameWork
{
    public class EfCoreCommentRepository: EfCoreGenericRepository<FarmerComment, FarmerContext>, ICommentRepository
    {
    }
}
