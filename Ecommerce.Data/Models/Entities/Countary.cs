

namespace Ecommerce.Data.Models.Entities
{
    public class Countary
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Address>? Addresses { get; set; }
    }
}
