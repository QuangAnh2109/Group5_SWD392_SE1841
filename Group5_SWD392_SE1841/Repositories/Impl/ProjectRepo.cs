using Group5_SWD392_SE1841.Models;
using Microsoft.EntityFrameworkCore;

namespace Group5_SWD392_SE1841.Repositories.Impl
{
    public class ProjectRepo : IProjectRepo
    {
        private readonly Group5Swd392Se1841Context _context;
        public ProjectRepo(Group5Swd392Se1841Context context)
        {
            _context = context;
        }
        public async Task<List<Project>> GetAssignedProjectsAsync(int employeeId)
        {
            return await _context.ProjectEmployees.Where(pe=>pe.EmployeeId==employeeId).Select(pe=>pe.Project).ToListAsync();
        }
    }
}
