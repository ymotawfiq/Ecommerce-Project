

namespace Ecommerce.Data.Models.Entities
{
    public class PaymentType
    {
        public Guid Id { get; set; }
        public string Value { get; set; } = string.Empty;
        public List<UserPaymentMethod>? UserPaymentMethods { get; set; }
    }
}
