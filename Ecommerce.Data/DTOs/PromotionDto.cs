

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.DTOs
{
    public class PromotionDto
    {
        public string? Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public float DiscountRate { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
