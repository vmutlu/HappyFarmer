using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.UI.Models
{
    public class ProductListModel
    {
        public PageInfo PageInfo { get; set; }
        public List<FarmerProduct> Products { get; set; }
    }
}
