

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.DTOs
{
    public class ProductVariationDto
    {
        public string? Id { get; set; }

        [Required]
        public Guid ProductItemId { get; set; }

        [Required]
        public Guid VariationOptionId { get; set; }
    }
}
