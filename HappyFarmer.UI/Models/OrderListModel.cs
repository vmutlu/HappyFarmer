using HappyFarmer.Entities;
using HappyFarmer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyFarmer.UI.Models
{
    public class OrderListModel
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
        public EnumOrderState OrderState { get; set; }
        public EnumPaymentTypes PaymentTypes { get; set; }
        public List<OrderItemModel> OrderItemModels { get; set; }

        public decimal TotalPrice()
        {
            return OrderItemModels.Sum(i => i.Price * i.Quantity);
        }
    }
}
