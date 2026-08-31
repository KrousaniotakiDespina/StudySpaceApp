using StudySpaceApp.Models;

namespace StudySpaceApp.DAO
{
    public interface ITodoTaskDAO
    {
        TodoTask? Insert(TodoTask todoTask);

        List<TodoTask> GetAllByUserId(int userId);

        bool Delete(int id, int userId);

        bool UpdateCompleted(int id, int userId, bool isCompleted);
    }
}