using HappyFarmer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyFarmer.UI.Models
{
    public class MultipleProductImagesModel
    {
        public int Id { get; set; }
        public FarmerProduct Product { get; set; }
        public int ProductId { get; set; }
        public List<string> ImageURL { get; set; }
    }
}
