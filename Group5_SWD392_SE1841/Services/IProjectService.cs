
using Group5_SWD392_SE1841.DTO;
using Group5_SWD392_SE1841.Models;

namespace Group5_SWD392_SE1841.Services
{
    public interface IProjectService
    {
        Task<List<ProjectDto>> GetAssignedProjectsAsync(int employeeId);
        public Task<PagedResult<ProjectDTO>> GetAllProjectByNamePagingAsync(int pageNumber, int pageSize, string? searchName, int? employeeId);
    }
}
