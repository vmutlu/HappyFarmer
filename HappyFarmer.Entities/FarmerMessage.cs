using HappyFarmer.Entities.Abstract;

namespace HappyFarmer.Entities
{
    // [Table("Chat Messages")]
    public class FarmerMessage : IBaseEntity<int>
    {
        public int Id { get; set; }
        public FarmerUser Users { get; set; }
        public int UserSenderId { get; set; }
        public int UserReceiverId { get; set; }
        public string Subject { get; set; }
        public string MessageContent { get; set; }
        public string MessageDate { get; set; }
        public bool MessageSendDelete { get; set; }
        public bool MessageReceiverDelete { get; set; }
    }
}
