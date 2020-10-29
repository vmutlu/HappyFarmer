using System.ComponentModel.DataAnnotations.Schema;

namespace HappyFarmer.API.DTOs
{
    public class FarmerMultipleProductsImagesDTO
    {
        public int Id { get; set; }

        [ForeignKey("FarmerProductDTO")]
        public int ProductId { get; set; }
        public FarmerProductDTO Product { get; set; }
        public string ImageURL { get; set; }
    }
}
