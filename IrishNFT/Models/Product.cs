using System.ComponentModel.DataAnnotations;

namespace IrishNFT.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required, StringLength(100)]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;

        public int? CategoryID { get; set; }

        public string CreatedBy { get; set; } = string.Empty;

        public string? ImageBase64 { get; set; } = string.Empty;

        public double? UnitPrice { get; set; }
        public int Status { get; set; }
        public Category Category { get; set; }
    }
}
