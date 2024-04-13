

using Ecommerce.Data.Models.Entities.Authentication;

namespace Ecommerce.Data.Models.Entities
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public SiteUser? User { get; set; } 
        public List<ShoppingCartItem>? ShoppingCartItems { get; set; }
    }
}
