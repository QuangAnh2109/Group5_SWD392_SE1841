namespace Group5_SWD392_SE1841.Services
{
    public interface ITaskService
    {
        Task<List<TaskDto>> GetAssignedTasksAsync(int employeeId, int? projectId = null);

    }
}
