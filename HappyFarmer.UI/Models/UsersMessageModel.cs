using HappyFarmer.Entities;

namespace HappyFarmer.UI.Models
{
    public class UsersMessageModel
    {
        public int Id { get; set; }
        public FarmerUser Users { get; set; }
        public int UserSenderId { get; set; }
        public int UserReceiverId { get; set; }
        public string Subject { get; set; }
        public string MessageContent { get; set; }
        public string MessageDate { get; set; }
    }
}
