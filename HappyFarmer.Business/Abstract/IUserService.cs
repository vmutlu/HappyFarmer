using HappyFarmer.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HappyFarmer.Business.Abstract
{
    public interface IUserService : IBaseService<FarmerUser>
    {
        List<FarmerUser> FindByEmail(string email);
        bool PasswordSignIn(string userEmail, string userPassword, int userType);
        List<FarmerUser> GetAllCustomer();
        List<FarmerUser> GetAllCarrier();
        List<FarmerUser> GetAllOnlyCustomer();
        List<FarmerOrderItem> GetUserSoldProduct(int userId);
        Task<FarmerUser> CreateCarrier(FarmerUser farmerUser, IFormFile file);
    }
}
