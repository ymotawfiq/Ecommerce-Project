

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.DTOs
{
    public class ShoppingCartItemDto
    {
        public string? Id { get; set; }

        [Required]
        public Guid CartId { get; set; }

        [Required]
        public Guid ProductItemId { get; set; }

        [Required]
        public int Qty { get; set; }
    }
}
