using UserManagementAPI.Services.Repositories;

namespace UserManagementAPI.Services;

public class EmailService(ISmtpRepository smtpRepository)
{
    
    public async Task SendConfirmationEmail(string userEmail)
    {
        await smtpRepository.SendEmailAsync(userEmail, "Confirm your email", "Please click the link to confirm your email.");
    }
    
    public async Task SendPasswordResetEmail(string userEmail)
    {
        await smtpRepository.SendEmailAsync(userEmail, "Reset your password", "Please click the link to reset your password.");
    }
}