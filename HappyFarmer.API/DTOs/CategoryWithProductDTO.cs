using System.Collections.Generic;

namespace HappyFarmer.API.DTOs
{
    public class CategoryWithProductDTO:FarmerCategoryDTO
    {
        public List<FarmerProductCategoryDTO> Products { get; set; }
    }
}
