using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.UI.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<FarmerProduct> Products { get; set; }
    }
}
