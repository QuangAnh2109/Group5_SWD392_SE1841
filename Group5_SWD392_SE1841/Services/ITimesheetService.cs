using Group5_SWD392_SE1841.Models;

namespace Group5_SWD392_SE1841.Services
{
    public interface ITimesheetService
    {

        Task<List<Timesheet>> GetFilteredTimesheetsAsync(int? employeeId, int? projectId, int? taskId, int? workStatusId, DateTime? startDate, DateTime? endDate);
        Task<Timesheet> AddTimesheetAsync(TimesheetEntryDto entry, int employeeId);
        Task<TimesheetSummaryDto> GetSummaryAsync(int employeeId, DateTime currentDate);
        Task<Timesheet> UpdateTimesheetAsync(TimesheetEntryDto entry, int timesheetId, int employeeId);
        Task<bool> DeleteTimesheetAsync(int timesheetId, int employeeId);
    }

    public class TimesheetEntryDto
    {
        public int TaskId { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Notes { get; set; }
    }

    public class TimesheetSummaryDto
    {
        public decimal HoursThisWeek { get; set; }
        public decimal HoursThisMonth { get; set; }
        public decimal AverageHoursPerDay { get; set; }
        public double ApprovalRate { get; set; }
    }

    public class TaskDto
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
    }
    public class ProjectDto
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
    }
}
