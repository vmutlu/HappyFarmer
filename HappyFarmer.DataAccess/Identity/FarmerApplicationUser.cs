using HappyFarmer.Entities;
using Microsoft.AspNetCore.Identity;

namespace HappyFarmer.DataAccess.Identity
{
    public class FarmerApplicationUser: IdentityUser
    {
        public virtual FarmerMessage Message { get; set; }
        public virtual FarmerGlobalMessage GlobalMessage { get; set; }
        public virtual FarmerProduct Product { get; set; }

    }
}
