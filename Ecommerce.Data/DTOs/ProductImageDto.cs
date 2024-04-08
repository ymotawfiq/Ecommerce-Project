

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.DTOs
{
    public class ProductImageDto
    {
        public Guid Id { get; set; }
        public string? ProductId { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public IFormFile? Image { get; set; }
    }
}
