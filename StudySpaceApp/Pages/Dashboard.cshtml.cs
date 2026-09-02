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
                return Unauthorized();
            }

            if (string.IsNullOrWhiteSpace(NewTaskTitle))
            {
                return BadRequest();
            }

            TodoTaskInsertDTO insertDTO =
                new TodoTaskInsertDTO
                {
                    Title = NewTaskTitle.Trim(),
                    UserId = userId.Value
                };

            TodoTaskReadOnlyDTO? newTask =
                _todoTaskService.Insert(insertDTO);

            if (newTask == null)
            {
                return BadRequest();
            }

            return new JsonResult(
                new
                {
                    id = newTask.Id,
                    title = newTask.Title,
                    isCompleted = newTask.IsCompleted
                }
            );
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

        public IActionResult OnPostUpdateTaskCompleted(
            int id,
            bool isCompleted)
        {
            int? userId =
                HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return Unauthorized();
            }

            bool updated =
                _todoTaskService.UpdateCompleted(
                    id,
                    userId.Value,
                    isCompleted
                );

            return new JsonResult(
                new { success = updated }
            );
        }

        public IActionResult OnPostAddNote()
        {
            int? userId =
                HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return Unauthorized();
            }

            if (string.IsNullOrWhiteSpace(NewNoteContent))
            {
                return BadRequest();
            }

            NoteInsertDTO insertDTO =
                new NoteInsertDTO
                {
                    Content = NewNoteContent.Trim(),
                    UserId = userId.Value
                };

            NoteReadOnlyDTO? newNote =
                _noteService.Insert(insertDTO);

            if (newNote == null)
            {
                return BadRequest();
            }

            return new JsonResult(
                new
                {
                    id = newNote.Id,
                    content = newNote.Content
                }
            );
        }

        public IActionResult OnPostDeleteNote(int id)
        {
            int? userId =
                HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return Unauthorized();
            }

            bool deleted =
                _noteService.Delete(
                    id,
                    userId.Value
                );

            return new JsonResult(
                new { success = deleted }
            );
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();

            return RedirectToPage("/Login");
        }
    }
}