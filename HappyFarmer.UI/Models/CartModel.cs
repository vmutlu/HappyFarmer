using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyFarmer.UI.Models
{
    public class CartModel
    {
        public int CartId { get; set; }
        public List<CartItemModel> CartItems { get; set; }

        public decimal TotalPrice()
        {
            return CartItems.Sum(i => i.Price * i.Quantity);
        }
    }
}
