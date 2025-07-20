using Group5_SWD392_SE1841.Models;

namespace Group5_SWD392_SE1841.Repositories
{
    public interface IProjectRepo
    {
        Task<List<Project>> GetAssignedProjectsAsync(int employeeId);
    }
}
