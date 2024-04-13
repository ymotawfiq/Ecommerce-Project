

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.DTOs
{
    public class UserPaymentMethodDto
    {
        public string? Id { get; set; }

        [Required]
        public string PaymentTypeId { get; set; } = string.Empty;

        [Required]
        public string UserIdOrEmail { get; set; } = string.Empty;

        [Required]
        public string Provider { get; set; } = string.Empty;

        [Required]
        public string AccountNumber { get; set; } = string.Empty;

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public bool IsDefault { get; set; }
    }
}
