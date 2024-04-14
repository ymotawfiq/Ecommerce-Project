

using Ecommerce.Data.Models.Entities.Authentication;

namespace Ecommerce.Data.Models.Entities
{
    public class ShopOrder
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public Guid PaymentMethodId { get; set; }
        public Guid ShippingAddressId { get; set; }
        public Guid ShippingMethodId { get; set; }
        public Guid OrderStatusId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderTotal { get; set; }
        public SiteUser? User { get; set; }
        public UserPaymentMethod? PaymentMethod { get; set; }
        public Address? Address { get; set; }
        public ShippingMethod? ShippingMethod { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public List<OrderLine>? OrderLines { get; set; }
    }
}
