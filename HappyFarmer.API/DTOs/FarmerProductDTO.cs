using HappyFarmer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HappyFarmer.API.DTOs
{
    public class FarmerProductDTO
    {
        public int Id { get; set; }
        public IEnumerable<FarmerProductCategoryDTO> CategoryDTOs { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int UserId { get; set; }
        public int FarmerDeclareType { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Neighborhood { get; set; }
        public DateTime AnnouncementDate { get; set; }
        public bool Guarantee { get; set; }
        public string Situation { get; set; }
        public bool Swap { get; set; }
        public bool PermissionToSell { get; set; }
        public int AnimalAge { get; set; }
    }
}
