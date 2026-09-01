using StudySpaceApp.Models;

namespace StudySpaceApp.DAO
{
    public interface INoteDAO
    {
        Note? Insert(Note note);

        List<Note> GetAllByUserId(int userId);

        bool Delete(int id, int userId);
    }
}