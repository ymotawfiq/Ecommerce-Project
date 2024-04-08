

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.DTOs
{
    public class VariationOptionsDto
    {
        public string? Id { get; set; }

        [Required]
        public string VariationId { get; set; } = string.Empty;

        [Required]
        public string Value { get; set; } = string.Empty;
    }
}
