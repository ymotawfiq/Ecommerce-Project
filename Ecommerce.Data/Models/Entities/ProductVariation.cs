
namespace Ecommerce.Data.Models.Entities
{
    public class ProductVariation
    {
        public Guid Id { get; set; }
        public Guid ProductItemId { get; set; }
        public Guid VariationOptionId { get; set; }
        public ProductItem? ProductItem { get; set; }
        public VariationOptions? VariationOption { get; set; }
    }
}
