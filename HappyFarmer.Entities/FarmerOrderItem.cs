using HappyFarmer.Entities.Abstract;

namespace HappyFarmer.Entities
{
    public class FarmerOrderItem : IBaseEntity<int>
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public FarmerOrder FarmerOrder { get; set; }
        public int ProductId { get; set; }
        public FarmerProduct FarmerProduct { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
