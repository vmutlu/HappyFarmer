using HappyFarmer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyFarmer.UI.Models
{
    public class ProductListModel
    {
        public PageInfo PageInfo { get; set; }
        public List<FarmerProduct> Products { get; set; }
    }
}
