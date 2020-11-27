
using HappyFarmer.Entities;
using System.Collections.Generic;

namespace HappyFarmer.UI.Models
{
    public class BlogModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatedDate { get; set; }
        public int QueueNumber { get; set; }
        public string ImagePath { get; set; }
        public int UserId { get; set; }
        public FarmerUser User { get; set; }
        public List<FarmerComment> Comments { get; set; }
    }
}
