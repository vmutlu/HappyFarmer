using HappyFarmer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyFarmer.API.DTOs
{
    public class FarmerUserDTO
    {
        public int Id { get; set; }
        public string TcNo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public int UserType { get; set; }
        public string ImageURL { get; set; }
        public string City { get; set; }
        public bool Authority { get; set; }
        public bool UserState { get; set; }
        public string RecordData { get; set; } = DateTime.Now.ToShortDateString();
        public ICollection<FarmerMessage> Messages { get; set; }
        public ICollection<FarmerGlobalMessage> GlobalMessages { get; set; }
        public ICollection<FarmerBlog> Blogs { get; set; }
        public ICollection<FarmerComment> Comments { get; set; }
        public ICollection<FarmerOrder> Orders { get; set; }
    }
}
