

namespace Ecommerce.Data.Models.Entities
{
    public class Address
    {
        public Guid Id { get; set; }
        public Guid CountaryId { get; set; }
        public int UnitNumber { get; set; }
        public int StreetNumber { get; set; }
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public Countary? Countary { get; set; }
        public List<UserAddress>? UserAddresses { get; set; }
        public List<ShopOrder>? ShopOrders { get; set; }
    }
}
