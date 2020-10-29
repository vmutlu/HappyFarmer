using HappyFarmer.Entities.Abstract;

namespace HappyFarmer.Entities
{
    public class FarmerCartItem : IBaseEntity<int>
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public FarmerProduct Product { get; set; }
        public int CartId { get; set; }
        public FarmerCart Cart { get; set; }
        public int Quantity { get; set; }
    }
}
