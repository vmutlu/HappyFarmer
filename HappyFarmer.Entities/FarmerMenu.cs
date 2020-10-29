using HappyFarmer.Entities.Abstract;

namespace HappyFarmer.Entities
{
    //[Table("Menu")]
    public class FarmerMenu : IBaseEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MenuURL { get; set; }
        public int SequenceNumber { get; set; }
        public bool Situation { get; set; }
    }
}
