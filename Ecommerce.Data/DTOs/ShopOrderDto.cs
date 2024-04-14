

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.DTOs
{
    public class ShopOrderDto
    {
        public string? Id { get; set; }

        [Required]
        public string UsernameOrEmail { get; set; } = string.Empty;

        [Required]
        public Guid PaymentMethodId { get; set; }

        [Required]
        public Guid ShippingAddressId { get; set; }

        [Required]
        public Guid ShippingMethodId { get; set; }

        [Required]
        public Guid OrderStatusId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public decimal OrderTotal { get; set; }
    }
}
