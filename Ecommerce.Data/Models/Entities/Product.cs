

using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Data.Models.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ProductImageUrl { get; set; } = string.Empty;
        public ProductCategory? Category { get; set; }
        public HashSet<ProductItem>? ProductItems { get; set; }
    }
}
