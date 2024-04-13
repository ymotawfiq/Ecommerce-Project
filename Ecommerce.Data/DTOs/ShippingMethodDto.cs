

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.DTOs
{
    public class ShippingMethodDto
    {
        public string? Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }
    }
}
