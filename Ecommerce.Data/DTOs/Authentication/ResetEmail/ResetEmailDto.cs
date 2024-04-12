

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.DTOs.Authentication.ResetEmail
{
    public class ResetEmailDto
    {
        [Required]
        //public string Password { get; set; } = string.Empty;
        //[Compare("Password", ErrorMessage = "The password and conformation password don't match...")]
        //public string ConfirmPassword { get; set; } = string.Empty;
        public string OldEmail { get; set; } = string.Empty;
        public string NewEmail { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
