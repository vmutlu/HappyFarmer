using HappyFarmer.Entities.Abstract;

namespace HappyFarmer.Entities
{
    public class FarmerProductCategory
    {
        public int CategoryId { get; set; }
        public FarmerCategory Category { get; set; }

        public int ProductId { get; set; }
        public FarmerProduct Product { get; set; }
    }
}
