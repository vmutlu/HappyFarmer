using System.ComponentModel.DataAnnotations;

namespace HappyFarmer.UI.Models
{
    public class AboutUsModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50,MinimumLength = 5)]
        public string Title { get; set; }

        [Required]
        [StringLength(5000, MinimumLength = 20)]
        public string Content { get; set; }
        public string Vision { get; set; }
        public string Mission { get; set; }
        public string VideoPath { get; set; }
    }
}
