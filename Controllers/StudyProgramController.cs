using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Contracts;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyProgramController(StudyProgramService service) : ControllerBase
    {
        //GET: api/StudyProgram
        [HttpGet]
        public async Task<IActionResult> GetStudyPrograms()
        {
            var studyPrograms = await service.GetAllPrograms();   

            return Ok(studyPrograms);
        }
    }
}