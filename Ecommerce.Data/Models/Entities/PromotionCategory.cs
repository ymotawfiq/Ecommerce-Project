
namespace Ecommerce.Data.Models.Entities
{
    public class PromotionCategory
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid PromotionId { get; set; }
        public ProductCategory? ProductCategory { get; set; }
        public Promotion? Promotion { get; set; }
    }
}
