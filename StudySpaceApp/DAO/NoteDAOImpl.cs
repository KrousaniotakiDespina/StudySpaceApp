using Microsoft.Data.SqlClient;
using StudySpaceApp.Helpers;
using StudySpaceApp.Models;

namespace StudySpaceApp.DAO
{
    public class NoteDAOImpl : INoteDAO
    {
        private readonly DBHelper _dbHelper;

        public NoteDAOImpl(DBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public Note? Insert(Note note)
        {
            using SqlConnection connection =
                _dbHelper.GetConnection();

            connection.Open();

            string sql =
                @"INSERT INTO Notes
                    (Content, CreatedAt, UserId)
                  VALUES
                    (@Content, @CreatedAt, @UserId);

                  SELECT SCOPE_IDENTITY();";

            using SqlCommand command =
                new SqlCommand(sql, connection);

            command.Parameters.AddWithValue(
                "@Content",
                note.Content
            );

            command.Parameters.AddWithValue(
                "@CreatedAt",
                note.CreatedAt
            );

            command.Parameters.AddWithValue(
                "@UserId",
                note.UserId
            );

            object? result =
                command.ExecuteScalar();

            if (result == null)
            {
                return null;
            }

            int newId =
                Convert.ToInt32(result);

            note.Id = newId;

            return note;
        }

        public List<Note> GetAllByUserId(int userId)
        {
            List<Note> notes =
                new List<Note>();

            using SqlConnection connection =
                _dbHelper.GetConnection();

            connection.Open();

            string sql =
                @"SELECT Id, Content, CreatedAt, UserId
                  FROM Notes
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
                Note note =
                    new Note
                    {
                        Id = reader.GetInt32(0),
                        Content = reader.GetString(1),
                        CreatedAt = reader.GetDateTime(2),
                        UserId = reader.GetInt32(3)
                    };

                notes.Add(note);
            }

            return notes;
        }

        public bool Delete(int id, int userId)
        {
            using SqlConnection connection =
                _dbHelper.GetConnection();

            connection.Open();

            string sql =
                @"DELETE FROM Notes
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
    }
}