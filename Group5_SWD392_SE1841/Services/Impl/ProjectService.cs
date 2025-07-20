using Group5_SWD392_SE1841.DTO;
using Group5_SWD392_SE1841.Repositories;
using Group5_SWD392_SE1841.Utils;

namespace Group5_SWD392_SE1841.Services.Impl
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepo _projectRepository;
        private readonly IMasterRepo _masterRepository;
        public ProjectService(IProjectRepo projectRepository, IMasterRepo masterRepository)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _masterRepository = masterRepository;
        }

        public async Task<PagedResult<ProjectDTO>> GetAllProjectByNamePagingAsync(int pageNumber, int pageSize, string? searchName, int? employeeId)
        {
            // Validate parameters
            if (pageNumber < 1) pageNumber = Constant.PAGE_NUMBER;
            if (pageSize < 1) pageSize = Constant.PAGE_SIZE;

            // Get projects and total count
            var projectsTask = await _projectRepository.GetAllProjectByNamePagingAsync(pageNumber, pageSize, searchName, employeeId);
            var totalCountTask = await _projectRepository.CountProjectsAsync(searchName, employeeId);
            var statusDictionaryTask = await _masterRepository.GetMasterDictionaryByTypeNameAsync(Constant.PROJECT_STATUS);

            var projects = projectsTask;
            var totalCount = totalCountTask;
            var statusDictionary = statusDictionaryTask;

            List<ProjectDTO> projectDtos = projects.Select(p => new ProjectDTO
            {
                ProjectId = p.ProjectId,
                ProjectName = p.ProjectName,
                ProjectStatus = statusDictionary.GetValueOrDefault(p.ProjectStatus),
                CreatedTime = p.CreatedTime,
            }).ToList();

            return new PagedResult<ProjectDTO>(projectDtos, pageNumber, pageSize, totalCount);
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
