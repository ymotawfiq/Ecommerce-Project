

namespace Ecommerce.Data.Models.EmailModel.Constants
{
    public static class ResponseMessages
    {
        public static string GetEmailSuccessMessage(string emailAddress)
        {
            return $"Email sent successfully to {emailAddress}";
        }
            

    }
}
