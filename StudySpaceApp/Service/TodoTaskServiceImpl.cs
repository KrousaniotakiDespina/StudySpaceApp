using StudySpaceApp.DAO;
using StudySpaceApp.DTO;
using StudySpaceApp.Models;

namespace StudySpaceApp.Service
{
    public class TodoTaskServiceImpl : ITodoTaskService
    {
        private readonly ITodoTaskDAO _todoTaskDAO;

        public TodoTaskServiceImpl(ITodoTaskDAO todoTaskDAO)
        {
            _todoTaskDAO = todoTaskDAO;
        }

        public TodoTaskReadOnlyDTO? Insert(TodoTaskInsertDTO insertDTO)
        {
            TodoTask todoTask = new TodoTask
            {
                Title = insertDTO.Title,
                IsCompleted = false,
                UserId = insertDTO.UserId
            };

            TodoTask? insertedTask =
                _todoTaskDAO.Insert(todoTask);

            if (insertedTask == null)
            {
                return null;
            }

            return new TodoTaskReadOnlyDTO
            {
                Id = insertedTask.Id,
                Title = insertedTask.Title,
                IsCompleted = insertedTask.IsCompleted,
                UserId = insertedTask.UserId
            };
        }

        public List<TodoTaskReadOnlyDTO> GetAllByUserId(int userId)
        {
            List<TodoTask> tasks =
                _todoTaskDAO.GetAllByUserId(userId);

            List<TodoTaskReadOnlyDTO> taskDTOs =
                new List<TodoTaskReadOnlyDTO>();

            foreach (TodoTask task in tasks)
            {
                TodoTaskReadOnlyDTO taskDTO =
                    new TodoTaskReadOnlyDTO
                    {
                        Id = task.Id,
                        Title = task.Title,
                        IsCompleted = task.IsCompleted,
                        UserId = task.UserId
                    };

                taskDTOs.Add(taskDTO);
            }

            return taskDTOs;
        }

        public bool Delete(int id, int userId)
        {
            return _todoTaskDAO.Delete(id, userId);
        }

        public bool UpdateCompleted(
            int id,
            int userId,
            bool isCompleted)
        {
            return _todoTaskDAO.UpdateCompleted(
                id,
                userId,
                isCompleted
            );
        }
    }
}