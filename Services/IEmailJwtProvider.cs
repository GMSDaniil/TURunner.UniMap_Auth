namespace UserManagementAPI.Services;

public interface IEmailJwtProvider
{
    string GenerateToken(string userId);

    string? GetUserId(string token);
}