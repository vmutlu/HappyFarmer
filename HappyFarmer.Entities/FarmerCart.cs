using HappyFarmer.Entities.Abstract;
using System.Collections.Generic;

namespace HappyFarmer.Entities
{
    public class FarmerCart : IBaseEntity<int>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public FarmerUser User { get; set; }
        public List<FarmerCartItem> CartItems { get; set; }
    }
}
