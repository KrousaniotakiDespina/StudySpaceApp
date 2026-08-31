using Microsoft.Data.SqlClient;
using StudySpaceApp.Helpers;
using StudySpaceApp.Models;

namespace StudySpaceApp.DAO
{
    public class TodoTaskDAOImpl : ITodoTaskDAO
    {
        private readonly DBHelper _dbHelper;

        public TodoTaskDAOImpl(DBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public TodoTask? Insert(TodoTask todoTask)
        {
            using SqlConnection connection =
                _dbHelper.GetConnection();

            connection.Open();

            string sql =
                @"INSERT INTO TodoTasks
                    (Title, IsCompleted, UserId)
                  VALUES
                    (@Title, @IsCompleted, @UserId);

                  SELECT SCOPE_IDENTITY();";

            using SqlCommand command =
                new SqlCommand(sql, connection);

            command.Parameters.AddWithValue(
                "@Title",
                todoTask.Title
            );

            command.Parameters.AddWithValue(
                "@IsCompleted",
                todoTask.IsCompleted
            );

            command.Parameters.AddWithValue(
                "@UserId",
                todoTask.UserId
            );

            object? result =
                command.ExecuteScalar();

            if (result == null)
            {
                return null;
            }

            int newId =
                Convert.ToInt32(result);

            todoTask.Id = newId;

            return todoTask;
        }

        public List<TodoTask> GetAllByUserId(int userId)
        {
            List<TodoTask> tasks =
                new List<TodoTask>();

            using SqlConnection connection =
                _dbHelper.GetConnection();

            connection.Open();

            string sql =
                @"SELECT Id, Title, IsCompleted, UserId
                  FROM TodoTasks
                  WHERE UserId = @UserId
                  ORDER BY Id DESC";

            using SqlCommand command =
                new SqlCommand(sql, connection);

            command.Parameters.AddWithValue(
                "@UserId",
                userId
            );

            using SqlDataReader reader =
                command.ExecuteReader();

            while (reader.Read())
            {
                TodoTask task =
                    new TodoTask
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        IsCompleted = reader.GetBoolean(2),
                        UserId = reader.GetInt32(3)
                    };

                tasks.Add(task);
            }

            return tasks;
        }

        public bool Delete(int id, int userId)
        {
            using SqlConnection connection =
                _dbHelper.GetConnection();

            connection.Open();

            string sql =
                @"DELETE FROM TodoTasks
                  WHERE Id = @Id
                  AND UserId = @UserId";

            using SqlCommand command =
                new SqlCommand(sql, connection);

            command.Parameters.AddWithValue(
                "@Id",
                id
            );

            command.Parameters.AddWithValue(
                "@UserId",
                userId
            );

            int rowsAffected =
                command.ExecuteNonQuery();

            return rowsAffected > 0;
        }

        public bool UpdateCompleted(
            int id,
            int userId,
            bool isCompleted)
        {
            using SqlConnection connection =
                _dbHelper.GetConnection();

            connection.Open();

            string sql =
                @"UPDATE TodoTasks
                  SET IsCompleted = @IsCompleted
                  WHERE Id = @Id
                  AND UserId = @UserId";

            using SqlCommand command =
                new SqlCommand(sql, connection);

            command.Parameters.AddWithValue(
                "@IsCompleted",
                isCompleted
            );

            command.Parameters.AddWithValue(
                "@Id",
                id
            );

            command.Parameters.AddWithValue(
                "@UserId",
                userId
            );

            int rowsAffected =
                command.ExecuteNonQuery();

            return rowsAffected > 0;
        }
    }
}