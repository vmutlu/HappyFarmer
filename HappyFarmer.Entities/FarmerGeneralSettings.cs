using HappyFarmer.Entities.Abstract;

namespace HappyFarmer.Entities
{
    public class FarmerGeneralSettings : IBaseEntity<int>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public string Title { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string District { get; set; } //ilçe
        public string Address { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
    }
}
