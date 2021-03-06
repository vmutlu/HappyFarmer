﻿using System.ComponentModel.DataAnnotations;

namespace HappyFarmer.UI.Models
{
    public class AdminMessageModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Subject { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; }
    }
}
