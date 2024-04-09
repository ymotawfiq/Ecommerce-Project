

namespace Ecommerce.Data.DTOs
{
    public class PromotionCategoryDto
    {
        public string? Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid PromotionId { get; set; }
    }
}
