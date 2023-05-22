using PetRegistry.Application.Common.Interfaces;
using PetRegistry.Domain.Entities;
using PetRegistry.Domain.Configuration;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using System.Text;

namespace PetRegistry.Domain.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfiguration;

        public EmailService(IOptions<EmailConfiguration> options)
        {
            _emailConfiguration = options.Value;
        }

        public async Task SendRegisterEmailAsync(User user, string verifyCode)
        {
            if (!_emailConfiguration.IsActive || string.IsNullOrEmpty(verifyCode) || string.IsNullOrEmpty(user?.Email))
                return;

            try
            {
                var message = new MailMessage
                {
                    Sender = new MailAddress(_emailConfiguration.Email!),
                    From = new MailAddress(_emailConfiguration.Email!)
                };

                message.To.Add(new MailAddress(user.Email!));
                message.Subject = "Your verification code";
                message.IsBodyHtml = true;
                message.Priority = MailPriority.Normal;
                message.SubjectEncoding = Encoding.UTF8;
                message.Body = $@"<h2>Your verification code</h2>
                <p>Hi, {user.Username}!<p>
                <p>Copy and paste this code into the app to validate your email address:</p>
                <h3 style=""letter-spacing: 12px;"">{verifyCode}</h3>
                <p style=""color:#6b6b78;"">If you didn’t request a verification code, please ignore this email.</p>
                <p>Thank you,</p>
                <p>The PetRegistry Team</p>";

                using var smtpClient = GetClient();
                await smtpClient.SendMailAsync(message);
            }
            catch (SmtpException ex)
            {
                throw ex;
            }
        }

        public async Task SendLockoutEmailAsync(User user)
        {
            if (!_emailConfiguration.IsActive || string.IsNullOrEmpty(user?.Email))
                return;

            try
            {
                var message = new MailMessage
                {
                    Sender = new MailAddress(_emailConfiguration.Email!),
                    From = new MailAddress(_emailConfiguration.Email!)
                };

                var lockoutEnd = user.LockoutEnd.HasValue ? user.LockoutEnd.Value.ToLocalTime() : (DateTime?)null;

                if (lockoutEnd == null)
                {
                    return;
                }

                message.To.Add(new MailAddress(user.Email!));
                message.Subject = "Your account has been locked out";
                message.IsBodyHtml = true;
                message.Priority = MailPriority.Normal;
                message.SubjectEncoding = Encoding.UTF8;
                message.Body = $@"<h2>Your account has been locked out</h2>
                <p>Hi, {user.Username}!<p>
                <p>Your lockout ends on: {lockoutEnd:dd/MM/yyyy HH:mm}</p>
                <p>The PetRegistry Team</p>";

                using var smtpClient = GetClient();
                await smtpClient.SendMailAsync(message);
            }
            catch (SmtpException ex)
            {
                throw ex;
            }
        }


        private SmtpClient GetClient()
        {
            return new SmtpClient
            {
                Host = _emailConfiguration.Host!,
                Port = _emailConfiguration.Port,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_emailConfiguration.Email, _emailConfiguration.Password)
            };
        }
    }

}
