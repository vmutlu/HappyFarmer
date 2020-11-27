namespace HappyFarmer.UI.Models
{
    public class OrderItemModel
    {
        public int OrderItemId { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public int Quantity { get; set; }
    }
}
