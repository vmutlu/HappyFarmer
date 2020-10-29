using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyFarmer.API.DTOs
{
    public class CategoryWithProductDTO:FarmerCategoryDTO
    {
        public List<FarmerProductCategoryDTO> Products { get; set; }
    }
}
