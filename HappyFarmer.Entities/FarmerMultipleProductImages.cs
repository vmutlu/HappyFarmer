using HappyFarmer.Entities.Abstract;

namespace HappyFarmer.Entities
{
    // [Table("MultipleProductImages")]
    public class FarmerMultipleProductImages : IBaseEntity<int>
    {
        public int Id { get; set; }
        public FarmerProduct Product { get; set; }
        public int ProductId { get; set; }
        public string ImageURL { get; set; }
    }
}
