

using Ecommerce.Data.Models.Entities.Authentication;

namespace Ecommerce.Data.Models.Entities
{
    public class UserReview
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public Guid OrderId { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; } = string.Empty;
        public SiteUser? User { get; set; }
        public OrderLine? OrderLine { get; set; }
    }
}
