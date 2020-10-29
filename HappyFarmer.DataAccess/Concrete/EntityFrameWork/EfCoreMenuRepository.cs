using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;

namespace HappyFarmer.DataAccess.Concrete.EntityFrameWork
{
    public class EfCoreMenuRepository : EfCoreGenericRepository<FarmerMenu, FarmerContext>, IMenuRepository
    {
    }
}
