using Group5_SWD392_SE1841.Models;

namespace Group5_SWD392_SE1841.Repositories
{
    public interface ITaskRepo
    {
        Task<List<Models.Task>> GetAssignedTasksAsync(int employeeId);
    }
}
