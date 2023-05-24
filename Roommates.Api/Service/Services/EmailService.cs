using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Roommates.Api.Service.Interfaces;
using Roommates.Api.Service.ViewModels;

namespace Roommates.Api.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendEmailAsync(EmailViewModel viewModel)
        {
            string emailHost = configuration.GetSection("EmailInfo:EmailHost").Value;
            int emailPort = int.Parse(configuration.GetSection("EmailInfo:EmailPort").Value);
            string emailUsername = configuration.GetSection("EmailInfo:EmailUsername").Value;
            string emailPassword = configuration.GetSection("EmailInfo:EmailPassword").Value;

            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(emailUsername));
            message.To.Add(MailboxAddress.Parse(viewModel.To));
            message.Subject = viewModel.Subject;
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = viewModel.Body
            };

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(emailHost, emailPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(emailUsername, emailPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch
            {
                await client.DisconnectAsync(true);
                throw;
            }
        }
    }
}
