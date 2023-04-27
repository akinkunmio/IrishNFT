﻿using System.ComponentModel.DataAnnotations;

namespace IrishNFT.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        public string? CategoryName { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;
    }
}
