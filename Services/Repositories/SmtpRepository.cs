using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace UserManagementAPI.Services.Repositories;

public class SmtpRepository(IOptions<SmtpOptions> options) : ISmtpRepository
{
    private readonly SmtpOptions _options = options.Value;
    
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var from = new MailAddress(_options.User, "Tubify");
        var toAddress = new MailAddress(to);

        using var smtp = new SmtpClient
        {
            Host = _options.Host,
            Port = _options.Port,
            EnableSsl = _options.EnableSsl,
            Credentials = new NetworkCredential(_options.User, _options.Pass)
        };
        
        using var message = new MailMessage(from, toAddress)
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };
        
        await smtp.SendMailAsync(message);
    }
}