using Group5_SWD392_SE1841.Models;
using Group5_SWD392_SE1841.Repositories;

namespace Group5_SWD392_SE1841.Services.Impl
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepo _projectRepository;
        public ProjectService(IProjectRepo projectRepository)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }

        public async Task<List<ProjectDto>> GetAssignedProjectsAsync(int employeeId)
        {
            if (employeeId <= 0)
                throw new ArgumentException("Invalid employee ID.");

            // Fetch projects from repository and map to DTOs
            var projects = await _projectRepository.GetAssignedProjectsAsync(employeeId);
            return projects.Select(p => new ProjectDto
            {
                ProjectId = p.ProjectId,
                ProjectName = p.ProjectName
            }).OrderBy(p => p.ProjectName).ToList();
        }
    }
}
