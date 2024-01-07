using ASP.NET_Custom_Identity_Starter.Settings;
using System.Net;
using System.Net.Mail;

namespace ASP.NET_Custom_Identity_Starter.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IOptions<SMTPSettings> _smtpSettings;
        public EmailSender(IOptions<SMTPSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings;
        }

        public async Task SendEmailAsync(string to, string subject, string htmlMessage)
        {
            var message = new MailMessage(_smtpSettings.Value.User,to,subject, htmlMessage);

            using(var smtp = new SmtpClient(_smtpSettings.Value.Host, 587))
            {
                smtp.Credentials = new NetworkCredential(_smtpSettings.Value.User, _smtpSettings.Value.Password);

                await smtp.SendMailAsync(message);
            }
        }
    }
}
