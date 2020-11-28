using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;

namespace HappyFarmer.DataAccess.Concrete.EntityFrameWork
{
    public class EfCoreAdminMessageRepository:EfCoreGenericRepository<FarmerAdminMessage,FarmerContext>,IAdminMessageRepository
    {
    }
}
