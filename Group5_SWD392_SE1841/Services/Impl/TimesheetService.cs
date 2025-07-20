using Group5_SWD392_SE1841.Models;
using Group5_SWD392_SE1841.Services;
using Microsoft.EntityFrameworkCore;

namespace Group5_SWD392_SE1841.Repositories
{
    public class TimesheetService : ITimesheetService

    {
        private readonly ITimesheetRepo _timesheetRepository;
        private readonly ITaskRepo _taskRepository;

        public TimesheetService(ITimesheetRepo timesheetRepository, ITaskRepo taskRepository)
        {
            _timesheetRepository = timesheetRepository;
            _taskRepository = taskRepository;
        }
        public async Task<bool> DeleteTimesheetAsync(int timesheetId, int employeeId)
        {
            if (employeeId <= 0)
                throw new ArgumentException("Invalid employee ID.");
            if (timesheetId <= 0)
                throw new ArgumentException("Invalid timesheet ID.");

            return await _timesheetRepository.DeleteTimesheetAsync(timesheetId, employeeId);
        }
        public async Task<List<Timesheet>> GetFilteredTimesheetsAsync(int? employeeId, int? projectId, int? taskId, int? workStatusId, DateTime? startDate, DateTime? endDate)
        {
            // Validate input parameters
            if (employeeId.HasValue && employeeId <= 0)
                throw new ArgumentException("Invalid employee ID.");
            if (taskId.HasValue && taskId <= 0)
                throw new ArgumentException("Invalid task ID.");
            if (projectId.HasValue && projectId <= 0)
                throw new ArgumentException("Invalid project ID.");
            if (workStatusId.HasValue && workStatusId < -1)
                throw new ArgumentException("Invalid work status ID.");
            if (startDate.HasValue && endDate.HasValue && startDate > endDate)
                throw new ArgumentException("Start date cannot be after end date.");

            var timesheets = await _timesheetRepository.GetTimesheetsAsync(employeeId, taskId, workStatusId, startDate, endDate);

            if (projectId.HasValue)
                timesheets = timesheets.Where(t => t.Task.ProjectId == projectId.Value).ToList();

            // Filter invalid end times
            timesheets = timesheets
                .Where(t => t.EndTime == null || t.EndTime > t.StartTime)
                .OrderByDescending(t => t.StartTime)
                .ToList();

            return timesheets;
        }
        public async Task<Timesheet> AddTimesheetAsync(TimesheetEntryDto entry, int employeeId)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));
            if (employeeId <= 0)
                throw new ArgumentException("Invalid employee ID.");
            if (string.IsNullOrEmpty(entry.StartTime) || string.IsNullOrEmpty(entry.EndTime))
                throw new ArgumentException("Start and end times are required.");

            var startDateTime = DateTime.Parse($"{entry.Date:yyyy-MM-dd} {entry.StartTime}");
            var endDateTime = DateTime.Parse($"{entry.Date:yyyy-MM-dd} {entry.EndTime}");
            if (endDateTime <= startDateTime)
                throw new ArgumentException("End time must be after start time.");

            var timesheet = new Timesheet
            {
                EmployeeId = employeeId,
                TaskId = entry.TaskId,
                WorkStatusId = 0, // Pending
                StartTime = startDateTime,
                EndTime = endDateTime,
                Notes = entry.Notes,
                RecordNo = Guid.NewGuid().ToByteArray(),
                CreatedId = employeeId,
                CreatedTime = DateTime.Now,
                UpdatesId = employeeId,
                UpdateTime = DateTime.Now,
                DeleteFlg = false
            };

            return await _timesheetRepository.AddTimesheetAsync(timesheet);
        }

        public async Task<Timesheet> UpdateTimesheetAsync(TimesheetEntryDto entry, int timesheetId, int employeeId)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));
            if (timesheetId <= 0)
                throw new ArgumentException("Invalid timesheet ID.");
            if (employeeId <= 0)
                throw new ArgumentException("Invalid employee ID.");
            if (string.IsNullOrEmpty(entry.StartTime) || string.IsNullOrEmpty(entry.EndTime))
                throw new ArgumentException("Start and end times are required.");

            var startDateTime = DateTime.Parse($"{entry.Date:yyyy-MM-dd} {entry.StartTime}");
            var endDateTime = DateTime.Parse($"{entry.Date:yyyy-MM-dd} {entry.EndTime}");
            if (endDateTime <= startDateTime)
                throw new ArgumentException("End time must be after start time.");

            var timesheet = new Timesheet
            {
                TimeSheetId = timesheetId,
                EmployeeId = employeeId,
                TaskId = entry.TaskId,
                WorkStatusId = 0, // Ensure remains Pending
                StartTime = startDateTime,
                EndTime = endDateTime,
                Notes = entry.Notes,
                UpdatesId = employeeId,
                UpdateTime = DateTime.Now,
                DeleteFlg = false
            };

            return await _timesheetRepository.UpdateTimesheetAsync(timesheet);
        }

        public async Task<TimesheetSummaryDto> GetSummaryAsync(int employeeId, DateTime currentDate)
        {
            if (employeeId <= 0)
                throw new ArgumentException("Invalid employee ID.");

            var startOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            return new TimesheetSummaryDto
            {
                HoursThisWeek = await _timesheetRepository.GetTotalHoursThisWeekAsync(employeeId, currentDate),
                HoursThisMonth = await _timesheetRepository.GetTotalHoursThisMonthAsync(employeeId, currentDate),
                AverageHoursPerDay = await _timesheetRepository.GetAverageHoursPerDayAsync(employeeId, startOfMonth, endOfMonth),
                ApprovalRate = await _timesheetRepository.GetApprovalRateAsync(employeeId, startOfMonth, endOfMonth)
            };
        }

        

    }
}
