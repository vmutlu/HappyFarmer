using System.ComponentModel.DataAnnotations;

namespace HappyFarmer.UI.Models
{
    public class SliderModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        public string ImagePath { get; set; }

        [Required]
        public int SequenceNumber { get; set; }

        [Required]
        public bool Situation { get; set; }
        public string Url { get; set; }
    }
}
