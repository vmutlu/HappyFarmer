using HappyFarmer.Entities.Abstract;
using System.Collections.Generic;

namespace HappyFarmer.Entities
{
    public class FarmerCategory : IBaseEntity<int>
    {
        public FarmerCategory()
        {
            ProductCategories = new List<FarmerProductCategory>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<FarmerProductCategory> ProductCategories { get; set; }
    }
}
