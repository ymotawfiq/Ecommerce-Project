

using Ecommerce.Data.Models.Entities.Authentication;

namespace Ecommerce.Data.DTOs.Authentication.User
{
    public class LoginOtpResponse
    {
        public string Token { get; set; } = null!;
        public bool IsTwoFactorEnable { get; set; }
        public SiteUser User { get; set; } = null!;
    }
}
