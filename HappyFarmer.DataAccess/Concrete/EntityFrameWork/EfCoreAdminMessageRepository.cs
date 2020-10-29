using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HappyFarmer.DataAccess.Concrete.EntityFrameWork
{
    public class EfCoreAdminMessageRepository:EfCoreGenericRepository<FarmerAdminMessage,FarmerContext>,IAdminMessageRepository
    {
    }
}
