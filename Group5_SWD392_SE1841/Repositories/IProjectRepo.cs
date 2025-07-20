using Group5_SWD392_SE1841.Models;

namespace Group5_SWD392_SE1841.Repositories
{
    public interface IProjectRepo
    {
        Task<List<Project>> GetAssignedProjectsAsync(int employeeId);
        public Task<List<Project>> GetAllProjectByNamePagingAsync(int pageNumber, int pageSize, string? searchName, int? employeeId);
        Task<int> CountProjectsAsync(string? searchName, int? employeeId);
    }
}
