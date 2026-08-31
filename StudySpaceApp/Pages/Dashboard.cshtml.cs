using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudySpaceApp.DTO;
using StudySpaceApp.Service;

namespace StudySpaceApp.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly ITodoTaskService _todoTaskService;

        public DashboardModel(ITodoTaskService todoTaskService)
        {
            _todoTaskService = todoTaskService;
        }

        public List<TodoTaskReadOnlyDTO> TodoTasks { get; set; } = new();

        [BindProperty]
        public string NewTaskTitle { get; set; } = null!;

        public IActionResult OnGet()
        {
            int? userId =
                HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            TodoTasks =
                _todoTaskService.GetAllByUserId(userId.Value);

            return Page();
        }

        public IActionResult OnPostAddTask()
        {
            int? userId =
                HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            if (string.IsNullOrWhiteSpace(NewTaskTitle))
            {
                return RedirectToPage();
            }

            TodoTaskInsertDTO insertDTO =
                new TodoTaskInsertDTO
                {
                    Title = NewTaskTitle.Trim(),
                    UserId = userId.Value
                };

            _todoTaskService.Insert(insertDTO);

            return RedirectToPage();
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();

            return RedirectToPage("/Login");
        }
    }
}