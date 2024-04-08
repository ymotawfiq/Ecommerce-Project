

namespace Ecommerce.Data.Models.Entities
{
    public class ProductImages
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductImageUrl { get; set; } = string.Empty;
        public Product? Product { get; set; }
    }
}
