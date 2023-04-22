using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using Roommates.Service.Interfaces;
using Roommates.Service.ViewModels;

namespace Roommates.Service.Services
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
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(""));
            message.To.Add(MailboxAddress.Parse(viewModel.To));
            message.Subject = viewModel.Subject;

            message.Body = new TextPart(TextFormat.Html)
            {
                Text = viewModel.Body
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(configuration.GetSection("EmailHost").Value, int.Parse(configuration.GetSection("EmailPort").Value), SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(configuration.GetSection("EmailUsername").Value, configuration.GetSection("EmailPassword").Value);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

        }
    }
}
