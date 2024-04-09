
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.DTOs
{
    public class CountaryDto
    {
        public string? Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
