

namespace Ecommerce.Data.Models.Entities
{
    public class ProductItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string SKU { get; set; } = string.Empty;
        public int QuantityInStock { get; set; }
        public string ProducItemImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Product? Product { get; set; }
        public HashSet<ProductVariation>? ProductVariation2 { get; set; }
        public List<ShoppingCartItem>? ShoppingCartItems { get; set; }
        public List<OrderLine>? OrderLines { get; set; }

    }
}
