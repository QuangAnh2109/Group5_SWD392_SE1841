using Group5_SWD392_SE1841.Models;
using Microsoft.EntityFrameworkCore;

namespace Group5_SWD392_SE1841.Repositories.Iplm
{
    public class TimesheetRepo : ITimesheetRepo
    {
        private readonly Group5Swd392Se1841Context _context;

        public TimesheetRepo(Group5Swd392Se1841Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddTimesheet(Timesheet timesheet)
        {
            if (timesheet == null) throw new ArgumentNullException(nameof(timesheet));
            _context.Timesheets.Add(timesheet);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IQueryable<Timesheet> GetAll()
        {
            return _context.Timesheets;
        }
        public async Task<List<Timesheet>> GetTimesheetsAsync(int? employeeId, int? taskId, int? workStatusId, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Timesheets
                .Include(t => t.Task)
                .ThenInclude(t => t.Project)
                .Include(t => t.Employee)
                .Where(t => !t.DeleteFlg);

            if (employeeId.HasValue)
                query = query.Where(t => t.EmployeeId == employeeId.Value);
            if (taskId.HasValue)
                query = query.Where(t => t.TaskId == taskId.Value);
            if (workStatusId.HasValue)
                query = query.Where(t => t.WorkStatusId == workStatusId.Value);
            if (startDate.HasValue)
                query = query.Where(t => t.StartTime.Date >= startDate.Value.Date);
            if (endDate.HasValue)
                query = query.Where(t => t.StartTime.Date <= endDate.Value.Date);

            return await query.OrderByDescending(t => t.StartTime).ToListAsync();
        }

        public async Task<Timesheet> AddTimesheetAsync(Timesheet timesheet)
        {
            _context.Timesheets.Add(timesheet);
            await _context.SaveChangesAsync();
            return timesheet;
        }

        public async Task<Timesheet> UpdateTimesheetAsync(Timesheet timesheet)
        {
            var existingTimesheet = await _context.Timesheets
                .FirstOrDefaultAsync(t => t.TimeSheetId == timesheet.TimeSheetId && !t.DeleteFlg);
            if (existingTimesheet == null)
                throw new ArgumentException("Timesheet not found or has been deleted.");
            if (existingTimesheet.WorkStatusId != 0) // Only allow editing if Pending (0)
                throw new ArgumentException("Only pending timesheets can be edited.");

            existingTimesheet.TaskId = timesheet.TaskId;
            existingTimesheet.StartTime = timesheet.StartTime;
            existingTimesheet.EndTime = timesheet.EndTime;
            existingTimesheet.Notes = timesheet.Notes;
            existingTimesheet.UpdatesId = timesheet.UpdatesId;
            existingTimesheet.UpdateTime = timesheet.UpdateTime;

            await _context.SaveChangesAsync();
            return existingTimesheet;
        }

        public async Task<decimal> GetTotalHoursThisWeekAsync(int employeeId, DateTime currentDate)
        {
            var startOfWeek = currentDate.Date.AddDays(-(int)currentDate.DayOfWeek);
            var endOfWeek = startOfWeek.AddDays(7);
            var hours = await _context.Timesheets
                .Where(t => !t.DeleteFlg && t.EmployeeId == employeeId && t.StartTime.Date >= startOfWeek && t.StartTime.Date < endOfWeek && t.EndTime.HasValue)
                .SumAsync(t => EF.Functions.DateDiffHour(t.StartTime, t.EndTime.Value) / 60m);
            return hours;
        }

        public async Task<decimal> GetTotalHoursThisMonthAsync(int employeeId, DateTime currentDate)
        {
            var startOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1);
            var hours = await _context.Timesheets
                .Where(t => !t.DeleteFlg && t.EmployeeId == employeeId && t.StartTime.Date >= startOfMonth && t.StartTime.Date < endOfMonth && t.EndTime.HasValue)
                .SumAsync(t => EF.Functions.DateDiffHour(t.StartTime, t.EndTime.Value) / 60m);
            return hours;
        }

        public async Task<decimal> GetAverageHoursPerDayAsync(int employeeId, DateTime startDate, DateTime endDate)
        {
            var days = (endDate.Date - startDate.Date).Days + 1;
            if (days <= 0) return 0;
            var hours = await _context.Timesheets
                .Where(t => !t.DeleteFlg && t.EmployeeId == employeeId && t.StartTime.Date >= startDate.Date && t.StartTime.Date <= endDate.Date && t.EndTime.HasValue)
                .SumAsync(t => EF.Functions.DateDiffHour(t.StartTime, t.EndTime.Value) / 60m);
            return hours / days;
        }

        public async Task<double> GetApprovalRateAsync(int employeeId, DateTime startDate, DateTime endDate)
        {
            var totalEntries = await _context.Timesheets
                .CountAsync(t => !t.DeleteFlg && t.EmployeeId == employeeId && t.StartTime.Date >= startDate.Date && t.StartTime.Date <= endDate.Date);
            if (totalEntries == 0) return 0;
            var acceptedEntries = await _context.Timesheets
                .CountAsync(t => !t.DeleteFlg && t.EmployeeId == employeeId && t.StartTime.Date >= startDate.Date && t.StartTime.Date <= endDate.Date && t.WorkStatusId == 1);
            return Math.Round((double)acceptedEntries / totalEntries * 100, 2);
        }

        public async Task<List<Models.Task>> GetAssignedTasksAsync(int employeeId)
        {
            return await _context.Tasks
                .Where(ta => ta.EmployeeId == employeeId)
                .Include(t => t.Project)
                .Where(t => !t.DeleteFlg)
                .ToListAsync();
        }
        public async Task<bool> DeleteTimesheetAsync(int timesheetId, int employeeId)
        {
            var timesheet = await _context.Timesheets
                .Include(t => t.Task)
                .ThenInclude(t => t.Project)
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(t => t.TimeSheetId == timesheetId && t.EmployeeId == employeeId && !t.DeleteFlg);

            if (timesheet == null)
                return false;

            if (timesheet.WorkStatusId != 0) // Only allow deletion if pending
                return false;

            timesheet.DeleteFlg = true; // Soft delete
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
