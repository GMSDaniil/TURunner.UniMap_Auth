namespace UserManagementAPI.Entities;

public class ResetCodeEntity
{
    
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int Code { get; set; }
    public DateTime Expires { get; set; }
    public bool IsUsed { get; set; } = false;
    
}