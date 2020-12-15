namespace HappyFarmer.API.DTOs
{
    public class FarmerBannerDTO
    {
        public int Id { get; set; }
        public string ImageURL { get; set; }
        public bool Active { get; set; }
        public int QueueNumber { get; set; }
        public bool? LowerUpper { get; set; }
    }
}
