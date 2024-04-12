

using Ecommerce.Data.Models.Entities.Authentication;

namespace Ecommerce.Data.Models.Entities
{
    public class UserAddress
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public Guid AddressId { get; set; }
        public bool IsDefault { get; set; }
        public SiteUser? User { get; set; }
        public Address? Address { get; set; }
    }
}
