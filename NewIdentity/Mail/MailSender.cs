using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using NewIdentity.Entity;

namespace NewIdentity.Mail
{
    public class MailSender : IEmailSender<User>
    {
        private readonly IEmailSender _emailSender = new NoOpEmailSender();
        public async Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
        {
            await _emailSender.SendEmailAsync(email, "Confirmational", $"<a href={confirmationLink}>Your link is here</a>");
        }

        public async Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
        {
            await _emailSender.SendEmailAsync(email, "ResetPasswordCode", $"<a href={resetCode}>Your link is here</a>");
        }

        public async Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
        {
            await _emailSender.SendEmailAsync(email, "ResetPasswordLink", $"<a href={resetLink}>Your link is here</a>");
        }
    }
}
