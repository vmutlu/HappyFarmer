using System;

namespace HappyFarmer.API.DTOs
{
    public class FarmerChatRoomDTO
    {
        public int ChatRoomId { get; set; }
        public string Name { get; set; }
        public DateTime MessageDatetime { get; set; }
    }
}
