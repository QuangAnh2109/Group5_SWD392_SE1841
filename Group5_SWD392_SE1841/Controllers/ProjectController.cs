using Group5_SWD392_SE1841.DTO;
using Group5_SWD392_SE1841.Services;
using Group5_SWD392_SE1841.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace Group5_SWD392_SE1841.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(IProjectService projectService, ILogger<ProjectController> logger)
        {
            _projectService = projectService;
            _logger = logger;
        }
        public async Task<IActionResult> Index(int pageNumber, int pageSize, string? searchName)
        {
            try
            {
                _logger.LogDebug("Getting project list with pageNumber: {PageNumber}, pageSize: {PageSize}, searchName: {SearchName}", pageNumber, pageSize, searchName);
                int? employeeId = null;
                if (!Utils.Constant.ADMIN_ROLE)
                {
                    employeeId = Utils.Constant.EMPLOYEE_ID;
                }
                _logger.LogDebug("Employee ID: {EmployeeId}", employeeId);
                var result = await _projectService.GetAllProjectByNamePagingAsync(pageNumber, pageSize, searchName, employeeId);
                _logger.LogDebug("Retrieved {Count} projects", result.Items.Count);
                ViewBag.SearchName = searchName;

                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting project list");
                return View(new PagedResult<ProjectDTO>());
            }
        }
    }
}
