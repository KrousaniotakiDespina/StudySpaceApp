using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StudySpaceApp.Pages
{
    public class DashboardModel : PageModel
    {
        public IActionResult OnGet()
        {
            int? userId =
                HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            return Page();
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();

            return RedirectToPage("/Login");
        }
    }
}