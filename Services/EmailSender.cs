using Microsoft.AspNetCore.Identity.UI.Services;

namespace ASP.NET_Custom_Identity_Starter.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}
