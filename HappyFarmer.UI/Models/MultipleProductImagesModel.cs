using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.UI.Models
{
    public class MultipleProductImagesModel
    {
        public int Id { get; set; }
        public FarmerProduct Product { get; set; }
        public int ProductId { get; set; }
        public List<string> ImageURL { get; set; }
    }
}
