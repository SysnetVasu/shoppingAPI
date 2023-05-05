using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CartItemDto
    {
        [Required]
        public string ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
        public string Uom { get; set; }
        //[Required]
        public string ThumbnailUrl { get; set; }

        [Required]
        public string CategoryId { get; set; }

        [Required]
        public string UnitId { get; set; }


    }
}
