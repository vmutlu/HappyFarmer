using HappyFarmer.Entities;

namespace HappyFarmer.API.DTOs
{
    public class FarmerProductDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserEMail { get; set; }
        public string UserPhoneNumber { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int UserId { get; set; }
        public FarmerUser User { get; set; }
        public int FarmerDeclareType { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Neighborhood { get; set; }
        public string AnnouncementDate { get; set; }
        public bool Guarantee { get; set; }
        public string Situation { get; set; }
        public bool Swap { get; set; }
        public bool PermissionToSell { get; set; }
        public int AnimalAge { get; set; }
        public bool Gender { get; set; }
        public int StockQty { get; set; }
    }
}
