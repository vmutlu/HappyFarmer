using System.Collections.Generic;

namespace HappyFarmer.API.DTOs
{
    public class ProductWithCategoryDTO:FarmerProductDTO
    {
        public IEnumerable<FarmerCategoryDTO> CategoryDTOs { get; set; }
    }
}
