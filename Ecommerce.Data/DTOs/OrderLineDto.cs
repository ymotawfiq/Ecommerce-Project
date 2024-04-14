

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.DTOs
{
    public class OrderLineDto
    {
        public string? Id { get; set; }

        [Required]
        public Guid ShopOrderId { get; set; }

        [Required]
        public Guid ProductItemId { get; set; }

        [Required]
        public int Qty { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
