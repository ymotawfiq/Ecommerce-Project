

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.DTOs
{
    public class PaymentTypeDto
    {
        public string? Id { get; set; }

        [Required]
        public string Value { get; set; } = string.Empty;
    }
}
