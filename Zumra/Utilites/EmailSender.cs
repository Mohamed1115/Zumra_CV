using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Zumra.IRepositories;
using Zumra.Models;
using Microsoft.Extensions.Options;

namespace Zumra.Utilites;

public class EmailSender : IEmailSender, Microsoft.AspNetCore.Identity.UI.Services.IEmailSender
{
    private readonly EmailSettings _emailSettings;

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var mail = new MimeMessage();
        mail.From.Add(MailboxAddress.Parse(_emailSettings.From));
        mail.To.Add(MailboxAddress.Parse(email));
        mail.Subject = subject;

        var builder = new BodyBuilder { HtmlBody = message };
        mail.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();
        // Port 587 → STARTTLS
        await smtp.ConnectAsync(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
        await smtp.SendAsync(mail);
        await smtp.DisconnectAsync(true);
    }
}