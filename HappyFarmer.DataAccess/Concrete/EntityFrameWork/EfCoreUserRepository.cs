using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace HappyFarmer.DataAccess.Concrete.EntityFrameWork
{
    public class EfCoreUserRepository : EfCoreGenericRepository<FarmerUser, FarmerContext>, IUserRepository
    {
        public List<FarmerUser> FindByEmail(string email)
        {
            using (var context = new FarmerContext())
            {
                var user = context.FarmerUser
                    .Where(i => i.Email.ToLower() == email.ToLower()).AsNoTracking().ToList();

                return user;
            }
        }

        public List<FarmerUser> GetAllCarrier()
        {
            using (var context = new FarmerContext())
            {
                var response = context.FarmerUser
                    .Where(i => i.UserType == 2) //sadece nakliyecileri getir
                    .AsNoTracking()
                    .ToList();

                return response;
            }
        }

        public List<FarmerUser> GetAllCustomer()
        {
            using (var context = new FarmerContext())
            {
                var response = context.FarmerUser
                    .AsNoTracking()
                    .Where(i => i.UserType != 1).ToList();

                return response;
            }
        }

        public List<FarmerUser> GetAllOnlyCustomer()
        {
            using (var context = new FarmerContext())
            {
                var response = context.FarmerUser
                    .Where(i => i.UserType == 0)
                    .AsNoTracking()
                    .ToList();

                return response;
            }
        }

        public bool PasswordSignIn(string userEmail, string userPassword, int userType)
        {
            bool userState = true; //aktif olanlar

            using(var context = new FarmerContext())
            {
                var userLogin = context.FarmerUser
                    .Where(i => i.Email.ToLower() == userEmail.ToLower())
                    .Where(i => i.Password.ToLower() == userPassword.ToLower())
                    .Where(i => i.UserType == userType)
                    .Where(i => i.UserState == userState)
                    .FirstOrDefault();

                if (userLogin != null)
                    return true;

                else
                    return false;
            }
        }
    }
}
