using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Data;
using UserManagementAPI.Entities;

namespace UserManagementAPI.Repositories;

public class StudyProgramRepository(UserDbContext dbContext) : IStudyProgramRepository
{
    public async Task<List<StudyProgramEntity>> GetAllPrograms()
    {
        var programs = await dbContext.ProgramCatalog.ToListAsync();
        return programs;
    }
}