using System.ComponentModel.DataAnnotations;

namespace HappyFarmer.UI.Models
{
    public class OrderModel
    {
        [Display(Name = "İsim")] //burada yazılan attributeler asp-for ile frontende aktarılır
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string CVV { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string OrderNote { get; set; }
        public CartModel CartModel { get; set; }
    }
}
