using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;

namespace HappyFarmer.DataAccess.Concrete.EntityFrameWork
{
    public class EfCoreAboutUsRepository : EfCoreGenericRepository<FarmerAboutUs, FarmerContext>, IAboutUsRepository
    {
    }
}
