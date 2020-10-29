using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.Business.Abstract
{
    public interface IUserService : IBaseService<FarmerUser>
    {
        List<FarmerUser> FindByEmail(string email);
        bool PasswordSignIn(string userEmail, string userPassword, int userType);
        List<FarmerUser> GetAllCustomer();
        List<FarmerUser> GetAllCarrier();
        List<FarmerUser> GetAllOnlyCustomer();
    }
}
