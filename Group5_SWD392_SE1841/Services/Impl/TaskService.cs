
using Group5_SWD392_SE1841.Repositories;

namespace Group5_SWD392_SE1841.Services.Impl
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepo _taskRepository;
        public TaskService(ITaskRepo taskRepository)
        {
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        }

        public async Task<List<TaskDto>> GetAssignedTasksAsync(int employeeId, int? projectId = null)
        {
            if (employeeId <= 0)
                throw new ArgumentException("Invalid employee ID.");
            if (projectId.HasValue && projectId <= 0)
                throw new ArgumentException("Invalid project ID.");

            // Fetch tasks from repository and map to DTOs
            var tasks = await _taskRepository.GetAssignedTasksAsync(employeeId);
            tasks = tasks
                .Where(t => !projectId.HasValue || t.ProjectId == projectId)
                .ToList();
            return tasks.Select(t => new TaskDto
            {
                TaskId = t.TaskId,
                TaskName = t.TaskName,
                ProjectId = t.ProjectId,
                ProjectName = t.Project.ProjectName
            }).OrderBy(t => t.TaskName).ToList();
        }

    }
}
