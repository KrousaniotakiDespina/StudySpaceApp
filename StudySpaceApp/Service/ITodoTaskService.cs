using StudySpaceApp.DTO;

namespace StudySpaceApp.Service
{
    public interface ITodoTaskService
    {
        TodoTaskReadOnlyDTO? Insert(TodoTaskInsertDTO insertDTO);

        List<TodoTaskReadOnlyDTO> GetAllByUserId(int userId);

        bool Delete(int id, int userId);

        bool UpdateCompleted(int id, int userId, bool isCompleted);
    }
}