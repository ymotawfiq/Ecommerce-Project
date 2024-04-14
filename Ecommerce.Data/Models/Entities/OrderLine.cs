

namespace Ecommerce.Data.Models.Entities
{
    public class OrderLine
    {
        public Guid Id { get; set; }
        public Guid ShopOrderId { get; set; }
        public Guid ProductItemId { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public ShopOrder? ShopOrder { get; set; }
        public ProductItem? ProductItem { get; set; }
        public List<UserReview>? UserReview { get; set; }
    }
}
