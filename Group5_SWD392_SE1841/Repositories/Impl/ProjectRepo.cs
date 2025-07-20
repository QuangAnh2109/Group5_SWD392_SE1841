using Group5_SWD392_SE1841.Models;
using Group5_SWD392_SE1841.DTO;
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

        public async Task<int> CountProjectsAsync(string? searchName, int? employeeId)
        {
            var query = BuildProjectQuery(searchName, employeeId);
            return await query.CountAsync();
        }

        public async Task<List<Project>> GetAllProjectByNamePagingAsync(int pageNumber, int pageSize, string? searchName, int? employeeId)
        {
            

            return await BuildProjectQuery(searchName, employeeId)
                .OrderBy(p => p.ProjectId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Project>> GetAssignedProjectsAsync(int employeeId)
        {
            return await _context.ProjectEmployees.Where(pe=>pe.EmployeeId==employeeId).Select(pe=>pe.Project).ToListAsync();
        }

        private IQueryable<Project> BuildProjectQuery(string? searchName, int? employeeId)
        {
            var query = _context.Projects
                .Where(p => !p.DeleteFlg);

            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(p => p.ProjectName.Contains(searchName));
            }

            if (employeeId.HasValue)
            {
                query = query.Where(p => p.ProjectEmployees
                    .Any(pe => pe.EmployeeId == employeeId &&
                              !pe.DeleteFlg &&
                              (pe.EndDate == null || pe.EndDate > DateTime.Now)));
            }

            return query;
        }
    }
}
