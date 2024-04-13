

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.DTOs
{
    public class OrderStatusDto
    {
        public string? Id { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty;
    }
}
