

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.DTOs
{
    public class VariationDto
    {
        public string? Id { get; set; }

        [Required]
        public string CatrgoryId { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

    }
}
