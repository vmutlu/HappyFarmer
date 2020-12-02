using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.DataAccess.Abstract
{
    public interface IBannerRepository : IRepository<FarmerBanner>
    {
        List<FarmerBanner> GetLowerAll();
        List<FarmerBanner> GetAdminBanner();
    }
}
