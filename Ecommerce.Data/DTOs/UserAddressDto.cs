

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.DTOs
{
    public class UserAddressDto
    {
        public string? Id { get; set; }

        [Required]
        public string UserIdOrEmail { get; set; } = string.Empty;

        [Required]
        public string AddressId { get; set; } = string.Empty;

        [Required]
        public bool IsDefault { get; set; }
    }
}
