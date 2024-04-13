

namespace Ecommerce.Data.Models.Entities
{
    public class ShoppingCartItem
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductItemId { get; set; }
        public int Qty { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
        public ProductItem? ProductItem { get; set; }
    }
}
