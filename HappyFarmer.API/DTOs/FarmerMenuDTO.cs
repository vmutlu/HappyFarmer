namespace HappyFarmer.API.DTOs
{
    public class FarmerMenuDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MenuURL { get; set; }
        public int SequenceNumber { get; set; }
        public bool Situation { get; set; }
    }
}
