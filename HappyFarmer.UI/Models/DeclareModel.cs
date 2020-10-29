using HappyFarmer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyFarmer.UI.Models
{
    public class DeclareModel
    {
        public int Id { get; set; }
        public FarmerUser User { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EMail { get; set; }
        public string PhoneNumber { get; set; }
        public int FarmerDeclareType { get; set; } //ilan türü hayvan, çiftçi vs
        public string Title { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Neighborhood { get; set; } //mahalle
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime AnnouncementDate { get; set; } //ilan tarihi
        public bool Guarantee { get; set; } //garanti
        public string Situation { get; set; } //durum
        public string ImagePath { get; set; }
        public bool Swap { get; set; } //takas
        public bool PermissionToSell { get; set; } //satış izni
        public int AnimalAge { get; set; }
        public bool Gender { get; set; } //cinsiyet
        public bool FromWhom { get; set; } //kimden
        public int StockQty { get; set; } //stok adet
    }
}
