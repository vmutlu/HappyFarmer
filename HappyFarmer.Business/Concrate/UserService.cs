using HappyFarmer.Business.Abstract;
using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.Business.Concrate
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void Create(FarmerUser entity)
        {
            _userRepository.Create(entity);
        }

        public void Delete(FarmerUser entity)
        {
            _userRepository.Delete(entity);
        }

        public List<FarmerUser> FindByEmail(string email)
        {
            return _userRepository.FindByEmail(email);
        }

        public List<FarmerUser> GetAll()
        {
            return _userRepository.GetAll();
        }

        public List<FarmerUser> GetAllCarrier()
        {
            return _userRepository.GetAllCarrier();
        }

        public List<FarmerUser> GetAllCustomer()
        {
            return _userRepository.GetAllCustomer();
        }

        public List<FarmerUser> GetAllOnlyCustomer()
        {
            return _userRepository.GetAllOnlyCustomer();
        }

        public FarmerUser GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public List<FarmerOrderItem> GetUserSoldProduct(int userId)
        {
            return _userRepository.GetUserSoldProduct(userId);
        }

        public bool PasswordSignIn(string userEmail, string userPassword, int userType)
        {
            return _userRepository.PasswordSignIn(userEmail, userPassword,userType);
        }

        public void Update(FarmerUser entity)
        {
            _userRepository.Update(entity);
        }
    }
}
