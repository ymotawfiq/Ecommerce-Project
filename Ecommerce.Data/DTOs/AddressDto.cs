

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.DTOs
{
    public class AddressDto
    {
        public string? Id { get; set; }

        [Required]
        public Guid CountaryId { get; set; }

        [Required]
        public int UnitNumber { get; set; }

        [Required]
        public int StreetNumber { get; set; }

        [Required]
        public string AddressLine1 { get; set; } = string.Empty;

        [Required]
        public string AddressLine2 { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string Region { get; set; } = string.Empty;

        [Required]
        public string PostalCode { get; set; } = string.Empty;
    }
}
