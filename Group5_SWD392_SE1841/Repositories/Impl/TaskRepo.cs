using Group5_SWD392_SE1841.Models;
using Microsoft.EntityFrameworkCore;

namespace Group5_SWD392_SE1841.Repositories.Impl
{
    public class TaskRepo : ITaskRepo
    {
        private readonly Group5Swd392Se1841Context _context;

        public TaskRepo(Group5Swd392Se1841Context context)
        {
            _context = context;
        }

        public async Task<List<Models.Task>> GetAssignedTasksAsync(int employeeId)
        {
            return await _context.Tasks.Include(t => t.Project)
                .Where(t => t.EmployeeId == employeeId).ToListAsync();
        }

    }
}
