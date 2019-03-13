using System.Threading.Tasks;

namespace CleverCore.WebApp.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
