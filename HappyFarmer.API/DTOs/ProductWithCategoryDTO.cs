using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyFarmer.API.DTOs
{
    public class ProductWithCategoryDTO:FarmerProductDTO
    {
        public IEnumerable<FarmerCategoryDTO> CategoryDTOs { get; set; }
    }
}
