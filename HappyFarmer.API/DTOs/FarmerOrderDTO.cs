using HappyFarmer.Entities;
using HappyFarmer.Entities.Enums;
using System.Collections.Generic;

namespace HappyFarmer.API.DTOs
{
    public class FarmerOrderDTO
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string OrderDate { get; set; }
        public int UserId { get; set; }
        public FarmerUser User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string OrderNote { get; set; }
        public string PaymentId { get; set; }
        public string PaymentToken { get; set; }
        public string ConversationId { get; set; } //ıyzico için gerekli alan
        public EnumOrderState OrderState { get; set; }
        public EnumPaymentTypes PaymentTypes { get; set; }
        public List<FarmerOrderItem> FarmerOrderItems { get; set; }
    }
}
