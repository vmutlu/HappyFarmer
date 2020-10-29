using HappyFarmer.Entities.Abstract;

namespace HappyFarmer.Entities
{
    public class FarmerDistrincs : IBaseEntity<int>
    {
        public int Id { get; set; }
        public string Distrinc { get; set; }
        public int CityId { get; set; }
        public virtual FarmerCities City { get; set; }
    }
}
