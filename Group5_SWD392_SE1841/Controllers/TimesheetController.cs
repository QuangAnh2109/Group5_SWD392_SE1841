using Group5_SWD392_SE1841.Models;
using Group5_SWD392_SE1841.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("[controller]")]
public class TimesheetController : Controller
{
    private readonly ITimesheetService _timesheetService;
    private readonly IProjectService _projectService;
    private readonly ITaskService _taskService;

    public TimesheetController(ITimesheetService timesheetService, IProjectService projectService, ITaskService taskService)
    {
        _timesheetService = timesheetService ?? throw new ArgumentNullException(nameof(timesheetService));
        _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
        _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
    }

    [Route("manage-timesheet")]
    public async Task<IActionResult> ManageTimesheet(int? projectId, int? taskId, int? workStatusId, DateTime? startDate, DateTime? endDate)
    {
        int employeeId = 1; // Replace with User.FindFirstValue(ClaimTypes.NameIdentifier)
        try
        {
            var timesheets = await _timesheetService.GetFilteredTimesheetsAsync(employeeId, projectId, taskId, workStatusId, startDate, endDate);
            var summary = await _timesheetService.GetSummaryAsync(employeeId, DateTime.Now);
            var projects = await _projectService.GetAssignedProjectsAsync(employeeId);
            var tasks = await _taskService.GetAssignedTasksAsync(employeeId, projectId);

            ViewBag.Summary = summary;
            ViewBag.ProjectList = projects;
            ViewBag.Tasks = tasks;
            ViewBag.WorkStatuses = new[]
            {
            new { WorkStatusId = -1, StatusName = "All Status" },
            new { WorkStatusId = 0, StatusName = "Pending" },
            new { WorkStatusId = 1, StatusName = "Accepted" },
            new { WorkStatusId = 2, StatusName = "Rejected" }
        };
            // Ensure ViewBag reflects the current selection
            ViewBag.SelectedProjectIdFilter = projectId ?? (int?)null; // Use null if not provided
            ViewBag.SelectedTaskId = taskId ?? (int?)null;
            ViewBag.SelectedWorkStatusId = workStatusId ?? (int?)null;
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

            return View(timesheets); // View should handle TimesheetDto list
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(new List<TimesheetEntryDto>()); // Return empty list of DTOs
        }
    }

    [HttpPost]
    [Route("add-entry")]
    public async Task<IActionResult> AddEntry([FromBody] TimesheetEntryDto entry)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        int employeeId = 1; // Replace with actual user ID
        try
        {
            var timesheet = await _timesheetService.AddTimesheetAsync(entry, employeeId);
            return Ok(new { success = true });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    [HttpPost]
    [Route("delete-entry/{timesheetId:int}")]
    public async Task<IActionResult> DeleteEntry(int timesheetId)
    {
        int employeeId = 1; // Replace with User.FindFirstValue(ClaimTypes.NameIdentifier)
        try
        {
            var success = await _timesheetService.DeleteTimesheetAsync(timesheetId, employeeId);
            if (success)
            {
                return Ok(new { success = true, message = "Timesheet deleted successfully." });
            }
            return BadRequest(new { success = false, message = "Unable to delete timesheet. Only pending entries can be deleted by the owner." });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }
    [HttpPost]
    [Route("edit-entry/{timesheetId:int}")]
    public async Task<IActionResult> EditEntry([FromBody] TimesheetEntryDto entry, [FromRoute] int timesheetId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        int employeeId = 1; // Replace with actual user ID
        try
        {
            var timesheet = await _timesheetService.UpdateTimesheetAsync(entry, timesheetId, employeeId);
            return Ok(new { success = true });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    private int GetCurrentEmployeeId()
    {
        // Implement logic to get current employee ID from authentication (e.g., User.Identity)
        return 1; // Placeholder
    }
}