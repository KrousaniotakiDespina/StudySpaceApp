using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudySpaceApp.DTO;
using StudySpaceApp.Service;

namespace StudySpaceApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;
        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public UserLoginDTO LoginDTO { get; set; } = new();

        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var user = _userService.Login(LoginDTO);

            if (user == null)
            {
                ErrorMessage = "Invalid email or password.";

                return Page();
            }

            HttpContext.Session.SetInt32("UserId", user.Id);

            HttpContext.Session.SetString(
                "Firstname",
                user.Firstname
            );

            HttpContext.Session.SetString(
                "Theme",
                user.Theme
            );

            return RedirectToPage("/Dashboard");
        }
    }
}