namespace UserManagementAPI.Contracts
{
    public class LoginUserResponse
    {
        required public string Username { get; set; }
        required public string AccessToken { get; set; }
        required public string RefreshToken { get; set; }
        
    }
}
