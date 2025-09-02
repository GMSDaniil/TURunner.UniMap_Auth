using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Data;
using UserManagementAPI.Entities;

namespace UserManagementAPI.Services.Repositories;

public interface IResetCodeRepository
{
    Task Add(ResetCodeEntity entity);
    Task<List<ResetCodeEntity>> GetByUserId(Guid userId);
    Task DeleteAll(Guid userId);
    
    Task MarkAsUsed(Guid userId, int code);
}

public class ResetCodeRepository(UserDbContext context) : IResetCodeRepository
{
    private UserDbContext _context = context;
    
    public async Task Add(ResetCodeEntity entity)
    {
        await _context.PasswordResetCodes.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task<List<ResetCodeEntity>> GetByUserId(Guid userId)
    {
        return await _context.PasswordResetCodes
            .AsNoTracking()
            .Where(e => e.Expires > DateTime.UtcNow && e.UserId == userId && !e.IsUsed)
            .ToListAsync();
    }

    public async Task DeleteAll(Guid userId)
    {
        await _context.PasswordResetCodes.Where(e => e.UserId == userId).ExecuteDeleteAsync();
        await _context.SaveChangesAsync();
    }
    
    public async Task MarkAsUsed(Guid userId, int code)
    {
        var resetCode = await _context.PasswordResetCodes
            .FirstOrDefaultAsync(rc => rc.UserId == userId && rc.Code == code && !rc.IsUsed && rc.Expires > DateTime.UtcNow);
        if (resetCode != null)
        {
            resetCode.IsUsed = true;
            await _context.SaveChangesAsync();
        }
        
    }
}