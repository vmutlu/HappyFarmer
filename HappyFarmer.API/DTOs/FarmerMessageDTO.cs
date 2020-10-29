using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyFarmer.API.DTOs
{
    public class FarmerMessageDTO
    {
        public int Id { get; set; }
        public FarmerChatRoomDTO ChatRoom { get; set; }

        [ForeignKey("FarmerChatRoomDTO")]
        public int ChatRoomId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime MessageDatetime { get; set; }
    }
}
