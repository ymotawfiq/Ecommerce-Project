namespace Ecommerce.Data.Models.Entities
{
    public class Variation
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ProductCategory? Category { get; set; } = new();
        public HashSet<VariationOptions>? VariationOptions { get; set; } = new();
    }
}
