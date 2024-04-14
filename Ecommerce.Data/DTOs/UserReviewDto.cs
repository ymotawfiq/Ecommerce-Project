

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.DTOs
{
    public class UserReviewDto
    {
        public string? Id { get; set; }

        [Required]
        public string UsernameOrEmail { get; set; } = string.Empty;

        [Required]
        public Guid OrderId { get; set; }

        [Required]
        [Range(1,5)]
        public int Rate { get; set; }

        [Required]
        public string Comment { get; set; } = string.Empty;
    }
}
