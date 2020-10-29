using HappyFarmer.Entities.Abstract;

namespace HappyFarmer.Entities
{
    public class FarmerAdminMessage : IBaseEntity<int>
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
