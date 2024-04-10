using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.DTOs
{
    public class ProductDto
    {
        public string? Id { get; set; } = string.Empty;

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public List<IFormFile>? Image { get; set; }
    }
}
