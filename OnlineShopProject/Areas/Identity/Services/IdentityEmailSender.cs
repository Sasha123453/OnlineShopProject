using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using OnlineShop.Db.Models;
using System.Net.Mail;

namespace OnlineShopProject.Areas.Identity.Services
{
    public class IdentityEmailSender : IEmailSender<User>
    {
        private readonly IEmailSender _emailSender;
        public IdentityEmailSender(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public async Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
        {
            await _emailSender.SendEmailAsync(email, "Подтверждение почты", $"Пройдите по ссылке для подтверждения почты {confirmationLink}");
        }

        public async Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
        {
            await _emailSender.SendEmailAsync(email, "Сброс пароля", $"Ваш код для сброса пароля {resetCode}");
        }

        public async Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
        {
            await _emailSender.SendEmailAsync(email, "Сброс пароля", $"Ссылка для сброса пароля {resetLink}");
        }
    }
}
