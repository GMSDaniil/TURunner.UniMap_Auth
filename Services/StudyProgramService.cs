using UserManagementAPI.Data;
using UserManagementAPI.Entities;
using UserManagementAPI.Repositories;

namespace UserManagementAPI.Services;

public class StudyProgramService(IStudyProgramRepository repository)
{
    public async Task<List<StudyProgramEntity>> GetAllPrograms()
    {
        return await repository.GetAllPrograms();
    }
}