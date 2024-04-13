

namespace Ecommerce.Data.Models.Entities
{
    public class ShippingMethod
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
