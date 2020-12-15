namespace HappyFarmer.API.DTOs
{
    public class FarmerAdminMessageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool Seen { get; set; } = false;
    }
}
