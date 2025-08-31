using UserManagementAPI.Repositories;
using UserManagementAPI.Services.Repositories;

namespace UserManagementAPI.Services;

public class EmailService(ISmtpRepository smtpRepository, IEmailJwtProvider emailJwtProvider, IUserRepository userRepository)
{
    
    public async Task SendConfirmationEmail(string userEmail)
    {
        var user = await userRepository.GetByEmail(userEmail);
        if (user == null) throw new Exception("User not found");
        var token = emailJwtProvider.GenerateToken(user.Id.ToString());
        
        var confirmLink = $"https://myapp.com/tubify/api/Users/verify?token={token}";
        var htmlBody = $@"
            <p>Welcome to Tubify!</p>
            <p>Please confirm your email by clicking the link below:</p>
            <p><a href='{confirmLink}'>Confirm my email</a></p>
        ";
        
        await smtpRepository.SendEmailAsync(userEmail, "Confirm your email", htmlBody);
    }
    
    public async Task SendPasswordResetEmail(string userEmail)
    {
        await smtpRepository.SendEmailAsync(userEmail, "Reset your password", "Please click the link to reset your password.");
    }
}