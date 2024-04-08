

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.DTOs
{
    public class ProductItemDto
    {
        public string? Id { get; set; }
        
        [Required]
        public string ProductId { get; set; } = string.Empty;

        [Required]
        public string SKU { get; set; } = string.Empty;

        [Required]
        public int QuantityInStock { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public IFormFile? Image { get; set; }
    }
}
