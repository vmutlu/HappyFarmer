using HappyFarmer.Entities.Abstract;

namespace HappyFarmer.Entities
{
    public class FarmerGlobalMessage : IBaseEntity<int>
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public FarmerUser User { get; set; }
        public string Subject { get; set; }
        public string MessageContent { get; set; }
        public string MessageDate { get; set; }
        public bool CheckStatus { get; set; } = false;
        public int CityId { get; set; }
        public virtual FarmerCities City { get; set; }
        public bool FarmerOrCarrier { get; set; }
    }
}
