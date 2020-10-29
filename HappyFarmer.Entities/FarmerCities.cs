using HappyFarmer.Entities.Abstract;
using System.Collections.Generic;

namespace HappyFarmer.Entities
{
    public class FarmerCities : IBaseEntity<int>
    {
        public int Id { get; set; }
        public string Cities { get; set; }
        public virtual ICollection<FarmerDistrincs> Distrincs { get; set; }
    }
}
