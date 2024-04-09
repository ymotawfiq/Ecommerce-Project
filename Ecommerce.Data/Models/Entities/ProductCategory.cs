

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.Models.Entities
{
    public class ProductCategory
    {
        public Guid Id { get; set; }
        public string? ParentCategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        public HashSet<Product>? Products { get; set; }

        public HashSet<Variation>? Variations { get; set; }
        public List<PromotionCategory>? PromotionCategories { get; set; }
    }
}
