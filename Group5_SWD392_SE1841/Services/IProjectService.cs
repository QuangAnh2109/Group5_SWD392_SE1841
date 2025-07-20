
using Group5_SWD392_SE1841.Models;

namespace Group5_SWD392_SE1841.Services
{
    public interface IProjectService
    {
        Task<List<ProjectDto>> GetAssignedProjectsAsync(int employeeId);
    }
}
