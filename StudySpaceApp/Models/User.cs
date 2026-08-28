namespace StudySpaceApp.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Firstname { get; set; } = null!;

        public string Lastname { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Theme { get; set; } = "blue";
    }
}