namespace StudySpaceApp.DTO
{
    public class TodoTaskReadOnlyDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public bool IsCompleted { get; set; }

        public int UserId { get; set; }
    }
}