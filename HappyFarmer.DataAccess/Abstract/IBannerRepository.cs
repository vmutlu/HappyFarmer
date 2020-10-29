using HappyFarmer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HappyFarmer.DataAccess.Abstract
{
    public interface IBannerRepository : IRepository<FarmerBanner>
    {
        List<FarmerBanner> GetLowerAll();
        List<FarmerBanner> GetAdminBanner();
    }
}
