namespace UserManagementAPI.Services;

public class EmailJwtOptions
{
    public string SecretKey { get; set; } = string.Empty;
    public int ExpiresHours { get; set; }
}