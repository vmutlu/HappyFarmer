using HappyFarmer.Entities;
using System;

namespace HappyFarmer.UI.Models
{
    public class GlobalMessageModel
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public FarmerUser User { get; set; }
        public string Subject { get; set; }
        public string MessageContent { get; set; }
        public DateTime MessageDate { get; set; }
        public bool CheckStatus { get; set; } = false;
        public string NameSurname { get; set; }
    }
}
