

namespace Ecommerce.Data.DTOs.Authentication.SMS
{
    public class SendSmsDto
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
