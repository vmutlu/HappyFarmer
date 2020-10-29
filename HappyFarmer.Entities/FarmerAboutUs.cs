using HappyFarmer.Entities.Abstract;

namespace HappyFarmer.Entities
{
    // [Table("About")]
    public class FarmerAboutUs: IBaseEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Vision { get; set; }
        public string Mission { get; set; }
        public string VideoPath { get; set; }
        public string ImagePath { get; set; }
    }
}
