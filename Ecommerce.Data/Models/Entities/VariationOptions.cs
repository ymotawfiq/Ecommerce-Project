

namespace Ecommerce.Data.Models.Entities
{
    public class VariationOptions
    {
        public Guid Id { get; set; }
        public Guid VariationId { get; set; }
        public string Value { get; set; } = string.Empty;
        public Variation? Variation { get; set; } = new();
        public HashSet<ProductVariation>? ProductVariation1 { get; set; } = new();
    }
}
