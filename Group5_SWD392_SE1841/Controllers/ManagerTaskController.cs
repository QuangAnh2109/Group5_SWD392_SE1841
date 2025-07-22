using Microsoft.AspNetCore.Mvc;
using Group5_SWD392_SE1841.Models;
using Microsoft.EntityFrameworkCore;
using Task = Group5_SWD392_SE1841.Models.Task;

namespace Group5_SWD392_SE1841.Controllers
{
    public class ManagerProjectTaskController : Controller
    {
        private readonly Group5Swd392Se1841Context _context;

        public ManagerProjectTaskController(Group5Swd392Se1841Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create(int projectId)
        {
            var project = _context.Projects
                .FirstOrDefault(p => p.ProjectId == projectId && !p.DeleteFlg);
            if (project == null) return NotFound();

            var allTasks = _context.Tasks
                .Where(t => t.ProjectId == projectId && !t.DeleteFlg)
                .Include(t => t.Employee)
                .ToList();

            var employees = _context.Employees
                .Where(e => !e.DeleteFlg)
                .Select(e => new { e.EmployeeId, e.EmployeeName })
                .ToList();

            ViewBag.ProjectId = projectId;
            ViewBag.ProjectName = project.ProjectName;
            ViewBag.Employees = employees;

            ViewBag.TasksByStatus = allTasks
                .GroupBy(t => t.TaskStatusId)
                .ToDictionary(g => g.Key, g => g.ToList());

            return View();
        }

        [HttpPost]
        public IActionResult Create(Task task, int projectId)
        {
            if (string.IsNullOrWhiteSpace(task.TaskName) || task.EmployeeId == null)
            {
                return RedirectToAction("Create", new { projectId });
            }

            task.ProjectId = projectId;
            task.CreatedTime = DateTime.Now;
            task.UpdatesId = 5; // placeholder
            task.UpdateTime = DateTime.Now;

            task.CreatedId = 5; // placeholder, sẽ dùng session sau
                                
            task.DeleteFlg = false;

            try
            {
                _context.Tasks.Add(task);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                return BadRequest("Lỗi khi lưu task: " + ex.Message);
            }

            return RedirectToAction("Create", new { projectId });
        }

    }
}
