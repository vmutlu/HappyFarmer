using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;

namespace HappyFarmer.DataAccess.Concrete.EntityFrameWork
{
    public class EfCoreGeneralSettingsRepository: EfCoreGenericRepository<FarmerGeneralSettings, FarmerContext>, IGeneralSettingsRepository
    {
    }
}
