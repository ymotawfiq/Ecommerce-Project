

namespace Ecommerce.Data.Models.Entities
{
    public class Promotion
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float DiscountRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<PromotionCategory>? PromotionCategories { get; set; }
    }
}
