

namespace Ecommerce.Data.Models.Entities
{
    public class OrderStatus
    {
        public Guid Id { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<ShopOrder>? ShopOrders { get; set; }
    }
}
