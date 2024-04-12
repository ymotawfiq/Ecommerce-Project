

using Ecommerce.Data.Models.MessageModel;

namespace Ecommerce.Service.Services.EmailService
{
    public interface IEmailService
    {
        string SendEmail(Message message);
    }
}
