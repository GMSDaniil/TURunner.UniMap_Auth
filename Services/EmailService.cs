using UserManagementAPI.Repositories;
using UserManagementAPI.Services.Repositories;

namespace UserManagementAPI.Services;

public class EmailService(ISmtpRepository smtpRepository, IEmailJwtProvider emailJwtProvider, IUserRepository userRepository, ResetCodesService resetCodeService)
{
    
    public async Task SendConfirmationEmail(string userEmail)
    {
        var user = await userRepository.GetByEmail(userEmail);
        if (user == null) throw new Exception("User not found");
        var token = emailJwtProvider.GenerateToken(user.Id.ToString());
        
        var confirmLink = $"https://dev.cherep.co/tubify/api/Users/verifyEmail?token={token}";
        var htmlBody = $@"
            <p>Welcome to Tubify!</p>
            <p>Please confirm your email by clicking the link below:</p>
            <p><a href='{confirmLink}'>Confirm my email</a></p>
        ";
        
        await smtpRepository.SendEmailAsync(userEmail, "Confirm your email", htmlBody);
    }

    public async Task ConfirmEmail(string token)
    {
        var userId = emailJwtProvider.GetUserId(token);
        if (userId == null) throw new Exception("Invalid token");
        var user = await userRepository.GetByUserId(userId);
        if (user == null) throw new Exception("User not found");
        await userRepository.ConfirmUser(user.Id.ToString());

    }
    
    public async Task SendPasswordResetEmail(string userEmail)
    {
        var user = await userRepository.GetByEmail(userEmail);
        if (user == null) throw new Exception("User not found");
        var resetCode = await resetCodeService.Generate(user.Id);

        var htmlBody = $@"
            <p>You have requested to reset your password.</p>
            <p>Your reset code is: <strong>{resetCode}</strong></p>
            <p>If you did not request a password reset, please ignore this email.</p>";
        
        await smtpRepository.SendEmailAsync(userEmail, "Reset your password", htmlBody);
    }
}