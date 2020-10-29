using HappyFarmer.Entities.Abstract;

namespace HappyFarmer.Entities
{
    // [Table("Slider")]
    public class FarmerSlider : IBaseEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int SequenceNumber { get; set; }
        public bool Situation { get; set; }
        public string Url { get; set; }
    }
}
