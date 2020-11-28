using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;

namespace HappyFarmer.DataAccess.Concrete.EntityFrameWork
{
    public class EfCoreSecurityInformationRepository : EfCoreGenericRepository<FarmerSecurityInformation, FarmerContext>, ISecurityInformationRepository
    {
    }
}
