using Group5_SWD392_SE1841.Models;

namespace Group5_SWD392_SE1841.Repositories
{
    public interface ITimesheetRepo
    {
        void AddTimesheet(Timesheet timesheet);
        void SaveChanges();
        IQueryable<Timesheet> GetAll();

        Task<List<Timesheet>> GetTimesheetsAsync(int? employeeId, int? taskId, int? workStatusId, DateTime? startDate, DateTime? endDate);
        Task<Timesheet> AddTimesheetAsync(Timesheet timesheet);

        Task<Timesheet> UpdateTimesheetAsync(Timesheet timesheet);
        Task<decimal> GetTotalHoursThisWeekAsync(int employeeId, DateTime currentDate);
        Task<decimal> GetTotalHoursThisMonthAsync(int employeeId, DateTime currentDate);
        Task<decimal> GetAverageHoursPerDayAsync(int employeeId, DateTime startDate, DateTime endDate);
        Task<double> GetApprovalRateAsync(int employeeId, DateTime startDate, DateTime endDate);
        Task<bool> DeleteTimesheetAsync(int timesheetId, int employeeId);
    }
}
