using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.API.DTOs
{
    public class FarmerCartDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public FarmerUser User { get; set; }
        public List<FarmerCartItem> CartItems { get; set; }
    }
}
