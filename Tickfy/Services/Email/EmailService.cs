using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.Mail;
using Tickfy.Settings;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Tickfy.Services.Email;

public class EmailService(IOptions<MailSettings> mailSettings) : IEmailSender
{
    private readonly MailSettings _mailSettings = mailSettings.Value;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage
        {
            Sender = MailboxAddress.Parse(_mailSettings.Mail),
            Subject = subject,
        };

        message.To.Add(MailboxAddress.Parse(email));

        var builder = new BodyBuilder
        {
            HtmlBody = htmlMessage
        };

        message.Body = builder.ToMessageBody();


        using var stmp = new SmtpClient();


        stmp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        stmp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
        await stmp.SendAsync(message);
        stmp.Disconnect(true);

    }
}