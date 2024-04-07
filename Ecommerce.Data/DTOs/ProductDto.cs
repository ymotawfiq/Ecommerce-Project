using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.DTOs
{
    public class ProductDto
    {
        public string? Id { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public string? ProductImageUrl { get; set; } = string.Empty;

        [Required]
        public IFormFile? Image { get; set; }
    }
}
