using HappyFarmer.Business.Abstract;
using HappyFarmer.DataAccess.Abstract;
using HappyFarmer.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HappyFarmer.Business.Concrate
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) =>
            _userRepository = userRepository;

        public void Create(FarmerUser entity) =>
            _userRepository.Create(entity);

        public async Task<FarmerUser> CreateCarrier(FarmerUser farmerUser, IFormFile file)
        {
            var userCarier = new FarmerUser()
            {
                Name = farmerUser.Name,
                Surname = farmerUser.Surname,
                Email = farmerUser.Email,
                PhoneNumber = farmerUser.PhoneNumber,
                Address = farmerUser.Address,
                Password = farmerUser.Password,
                UserType = farmerUser.UserType,
                City = farmerUser.City,
                RecordData = farmerUser.RecordData
            };

            if (file != null)
            {
                var incerrectImage = Path.GetExtension(file.FileName);
                if (incerrectImage == ".jpg" || incerrectImage == ".jpeg" || incerrectImage == ".png" || incerrectImage == ".bmp" || incerrectImage == ".gif" || incerrectImage == ".tiff")
                {
                    Random rastgele = new Random();
                    string harfler = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZabcçdefgğhıijklmnoöprsştuüvyz";
                    string uret = "";
                    for (int i = 0; i < 6; i++)
                    {
                        uret += harfler[rastgele.Next(harfler.Length)];
                    }

                    userCarier.ImageURL = uret + ".jpg";
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\CustomerImages", uret + ".jpg");

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream).ConfigureAwait(false);
                    }
                }

                else
                {
                    throw new ArgumentNullException("Hata !!! Desteklenmeyen dosya uzantısı yüklemeye çalıştınız lütfen yükleyeceginiz dosyanın uzantısının \"jpg, png, jpeg, tiff, bmp\" oldugundan emin olunuz...");
                }
            }

            return userCarier;
        }

        public void Delete(FarmerUser entity) =>
            _userRepository.Delete(entity);

        public List<FarmerUser> FindByEmail(string email) =>
             _userRepository.FindByEmail(email);

        public List<FarmerUser> GetAll() =>
             _userRepository.GetAll();

        public List<FarmerUser> GetAllCarrier() =>
             _userRepository.GetAllCarrier();

        public List<FarmerUser> GetAllCustomer() =>
             _userRepository.GetAllCustomer();

        public List<FarmerUser> GetAllOnlyCustomer() =>
             _userRepository.GetAllOnlyCustomer();

        public FarmerUser GetById(int id) =>
             _userRepository.GetById(id);

        public List<FarmerOrderItem> GetUserSoldProduct(int userId) =>
             _userRepository.GetUserSoldProduct(userId);

        public bool PasswordSignIn(string userEmail, string userPassword, int userType) =>
             _userRepository.PasswordSignIn(userEmail, userPassword, userType);

        public void Update(FarmerUser entity) =>
            _userRepository.Update(entity);
    }
}
