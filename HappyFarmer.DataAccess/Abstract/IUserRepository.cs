﻿using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.DataAccess.Abstract
{
    public interface IUserRepository : IRepository<FarmerUser>
    {
        List<FarmerUser> FindByEmail(string email);
        bool PasswordSignIn(string userEmail, string userPassword, int userType);
        List<FarmerUser> GetAllCustomer();
        List<FarmerUser> GetAllCarrier();
        List<FarmerUser> GetAllOnlyCustomer();
        List<FarmerOrderItem> GetUserSoldProduct(int userId);
    }
}
