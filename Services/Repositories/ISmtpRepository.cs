namespace UserManagementAPI.Services.Repositories;

public interface ISmtpRepository
{
    public Task SendEmailAsync(string to, string subject, string body);
}