namespace StudySpaceApp.DTO
{
    public class NoteReadOnlyDTO
    {
        public int Id { get; set; }

        public string Content { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
    }
}