

using Ecommerce.Data.Models.Entities.Authentication;

namespace Ecommerce.Data.Models.Entities
{
    public class UserPaymentMethod
    {
        public Guid Id { get; set; }
        public Guid PaymentTypeId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Provider { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public bool IsDefault { get; set; }
        public SiteUser? User { get; set; }
        public PaymentType? PaymentType { get; set; }
        public List<ShopOrder>? ShopOrders { get; set; }
    }
}
