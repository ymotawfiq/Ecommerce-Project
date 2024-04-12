
using Ecommerce.Data.Models.Entities.Authentication;

namespace Ecommerce.Data.DTOs.Authentication.User
{
    public class CreateUserResponse
    {
        public string Token { get; set; } = null!;
        public SiteUser User { get; set; } = null!;
    }
}
