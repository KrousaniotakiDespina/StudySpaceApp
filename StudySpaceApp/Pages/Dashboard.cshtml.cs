using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudySpaceApp.DTO;
using StudySpaceApp.Service;

namespace StudySpaceApp.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly ITodoTaskService _todoTaskService;
        private readonly INoteService _noteService;

        public DashboardModel(ITodoTaskService todoTaskService, INoteService noteService)
        {
            _todoTaskService = todoTaskService;
            _noteService = noteService;
        }

        public List<TodoTaskReadOnlyDTO> TodoTasks { get; set; } = new();

        [BindProperty]
        public string NewTaskTitle { get; set; } = null!;

        public List<NoteReadOnlyDTO> Notes { get; set; } = new();

        [BindProperty]
        public string NewNoteContent { get; set; } = null!;

        public string Firstname { get; set; } = null!;

        public IActionResult OnGet()
        {
            int? userId =
                HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            Firstname =
                HttpContext.Session.GetString("Firstname") ?? "";

            TodoTasks =
                _todoTaskService.GetAllByUserId(userId.Value);
            Notes =
                _noteService.GetAllByUserId(userId.Value);

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

        public IActionResult OnPostDeleteTask(int id)
        {
            int? userId =
                HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return Unauthorized();
            }

            bool deleted =
                _todoTaskService.Delete(
                    id,
                    userId.Value
                );

            return new JsonResult(
                new { success = deleted }
            );
        }

        public IActionResult OnPostUpdateTaskCompleted(int id, bool isCompleted)
        {
            int? userId =
                HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            _todoTaskService.UpdateCompleted(
                id,
                userId.Value,
                isCompleted
            );

            return RedirectToPage();
        }

        public IActionResult OnPostAddNote()
        {
            int? userId =
                HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            if (string.IsNullOrWhiteSpace(NewNoteContent))
            {
                return RedirectToPage();
            }

            NoteInsertDTO insertDTO =
                new NoteInsertDTO
                {
                    Content = NewNoteContent.Trim(),
                    UserId = userId.Value
                };

            _noteService.Insert(insertDTO);

            return RedirectToPage();
        }

        public IActionResult OnPostDeleteNote(int id)
        {
            int? userId =
                HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            _noteService.Delete(
                id,
                userId.Value
            );

            return RedirectToPage();
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();

            return RedirectToPage("/Login");
        }
    }
}