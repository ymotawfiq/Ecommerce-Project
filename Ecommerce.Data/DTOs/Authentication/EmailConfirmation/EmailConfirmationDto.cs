

using Ecommerce.Data.Models.Entities.Authentication;

namespace Ecommerce.Data.DTOs.Authentication.EmailConfirmation
{
    public class EmailConfirmationDto
    {
        public SiteUser User { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
