using HappyFarmer.Entities.Abstract;
using System.Collections.Generic;

namespace HappyFarmer.Entities
{
    public class FarmerBlog : IBaseEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatedDate { get; set; }
        public int QueueNumber { get; set; }
        public string ImagePath { get; set; }
        public int UserId { get; set; }
        public FarmerUser User { get; set; }
        public virtual ICollection<FarmerComment> Comments { get; set; }
    }
}
