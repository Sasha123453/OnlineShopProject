using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.Extensions.Options;
using OnlineShopProject.Models;
using System.Net.Mail;

namespace OnlineShopProject.Areas.Identity.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IOptionsMonitor<SmtpConfiguration> _smtpConfiguration;
        public EmailSender(IOptionsMonitor<SmtpConfiguration> smtpConfiguration)
        {
            _smtpConfiguration = smtpConfiguration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailMessage message = new MailMessage
            {
                From = new MailAddress(_smtpConfiguration.CurrentValue.UserName),
                Subject = subject,
                Body = htmlMessage,
            };
            message.To.Add(new MailAddress(email));
            SmtpClient smtpClient = new SmtpClient
            {
                EnableSsl = true,
                Credentials = new System.Net.NetworkCredential(_smtpConfiguration.CurrentValue.UserName, _smtpConfiguration.CurrentValue.Password),
                Host = _smtpConfiguration.CurrentValue.Host,
                Port = _smtpConfiguration.CurrentValue.Port,
                
            };
            await smtpClient.SendMailAsync(message);
        }
    }
}
