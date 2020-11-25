using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Protaru.Helper
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly ILogger<SmtpEmailSender> _logger;
        private readonly SmtpOptions _options;
        private readonly SmtpClient _client;

        public SmtpEmailSender(
            ILogger<SmtpEmailSender> logger,
            IOptions<SmtpOptions> options)
        {
            _logger = logger;
            _options = options.Value;
            _client = new SmtpClient
            {
                Host = _options.Host,
                Port = _options.Port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = _options.UseSSL
            };

            if (!string.IsNullOrEmpty(_options.Password))
            {
                _client.Credentials = new System.Net.NetworkCredential(
                    _options.Login,
                    _options.Password);
            }
            else
            {
                _client.UseDefaultCredentials = true;
            }
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            _logger.LogInformation($"Sending email: {email}, subject: {subject}, message: {htmlMessage}");

            try
            {
                string from = string.IsNullOrEmpty(_options.From) ?
                    _options.Login :
                    _options.From;
                MailMessage mail = new MailMessage(from, email)
                {
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = htmlMessage
                };

                _client.Send(mail);
                _logger.LogInformation($"Email: {email}, subject: {subject}, message: {htmlMessage} successfully sent");

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception {ex} during sending email: {email}, subject: {subject}");
                throw;
            }
        }
    }
}
