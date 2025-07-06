using UserManagementAPI.Entities;

namespace UserManagementAPI.Repositories;

public interface IStudyProgramRepository
{
    public Task<List<StudyProgramEntity>> GetAllPrograms();
}