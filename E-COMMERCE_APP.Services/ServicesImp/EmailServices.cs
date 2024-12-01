using E_COMMERCE_APP.Services.Abstracts;
using MimeKit;
using MailKit.Net.Smtp;
using E_COMMERCE_APP.Data.Helpers;
using MailKit.Security;

namespace E_COMMERCE_APP.Services.ServicesImp
{
    public class EmailServices : IEmailServices
    {
        private readonly EmailSettings emailSettings;

        public EmailServices(EmailSettings emailSettings)
        {
            this.emailSettings = emailSettings;
        }

        public async Task<string> SendEmailAsync(string email, string Message, string? reason)
        {
            try
            {
                //sending the Message of passwordResetLink
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(emailSettings.Host, emailSettings.Port, SecureSocketOptions.StartTls);
                    client.Authenticate(emailSettings.FromEmail, "uyvrpvrgshmxgmqg");

                    var bodybuilder = new BodyBuilder
                    {
                        HtmlBody = $"{Message}",
                        TextBody = "welcome",
                    };

                    var message = new MimeMessage
                    {
                        Body = bodybuilder.ToMessageBody()
                    };

                    message.From.Add(new MailboxAddress("Future Team", emailSettings.FromEmail));
                    message.To.Add(new MailboxAddress("user", email));
                    message.Subject = reason == null ? "No Submitted" : reason;
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
                //end of sending email
                return "Success";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "***************************************");
                return "Failed";
            }
        }

    }
}
