using ILearnSmartProject.Models;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;


namespace ILearnSmartProject.Services
{
    public class EmailAppService
    {

        private readonly SMTPConnection _appSettings;
        public EmailAppService(IOptions<SMTPConnection> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<string> SendEmailAsync(string message, string emailAddress)
        {
            // Simulate sending an email by printing the message to the console
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var emailContent = $"[Email Notification] {timestamp}: {message}";

            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse("noreply@ismart.com"));
            email.To.Add(MailboxAddress.Parse(emailAddress));
            email.Subject = "iLearnSmart Notification";
            email.Body = new TextPart(TextFormat.Plain) { Text = emailContent };
        

            using var smtp = new SmtpClient();
            smtp.Connect(_appSettings.Host, _appSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_appSettings.Username, _appSettings.Password); // Use environment variables or secure vault for credentials
            // email using MimeKit and mail kit
            smtp.Send(email);
            smtp.Disconnect(true);
            return "done";
        }
    }
}
