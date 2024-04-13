

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.DTOs
{
    public class ShoppingCartDto
    {
        public string? Id { get; set; }

        [Required]
        public string UserIdOrEmail { get; set; } = string.Empty;
    }
}
