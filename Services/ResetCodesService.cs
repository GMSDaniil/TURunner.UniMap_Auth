using UserManagementAPI.Entities;
using UserManagementAPI.Services.Repositories;

namespace UserManagementAPI.Services;

public class ResetCodesService(IResetCodeRepository resetCodeRepository)
{
    public async Task<int> Generate(Guid userId)
    {
        await resetCodeRepository.DeleteAll(userId);
        
        var code = new Random().Next(10000, 99999);
        ResetCodeEntity resetCode = new ResetCodeEntity()
        {
            UserId = userId,
            Code = code,
            Expires = DateTime.Now.AddMinutes(30),
        };
        
        await resetCodeRepository.Add(resetCode);
        
        return code;
    }
    
    public async Task<bool> Verify(Guid userId, int code)
    {
        var resetCodes = await resetCodeRepository.GetByUserId(userId);
        var resetCode = resetCodes.FirstOrDefault(rc => rc.Code == code && !rc.IsUsed && rc.Expires > DateTime.Now);
        if (resetCode == null)
        {
            return false;
        }
        else
        {
            await resetCodeRepository.MarkAsUsed(resetCode.UserId, resetCode.Code);
            return true;
        }
    }
    
}