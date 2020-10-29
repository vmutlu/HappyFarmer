using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.Business.Abstract
{
    public interface IBannerService : IBaseService<FarmerBanner>
    {
        List<FarmerBanner> GetLowerAll();
        List<FarmerBanner> GetAdminBanner();
    }
}
