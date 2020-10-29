using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyFarmer.API.DTOs
{
    public class FarmerProductCategoryDTO
    {
        public int CategoryId { get; set; }
        public FarmerCategoryDTO Category { get; set; }

        public int ProductId { get; set; }
        public FarmerProductDTO Product { get; set; }
    }
}
