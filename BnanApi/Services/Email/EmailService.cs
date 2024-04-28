using BnanApi.DTOS;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace BnanApi.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> SendEmail(EmailDTO request)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                // Exception Details
                return false;
            }
        }
    }
}
