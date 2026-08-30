using Microsoft.Data.SqlClient;
using StudySpaceApp.Helpers;
using StudySpaceApp.Models;

namespace StudySpaceApp.DAO
{
    public class UserDAOImpl : IUserDAO
    {
        private readonly DBHelper _dbHelper;

        public UserDAOImpl(DBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public User? GetUserByEmail(string email)
        {
            using SqlConnection connection =
                _dbHelper.GetConnection();

            connection.Open();

            string sql =
                @"SELECT Id, Firstname, Lastname, Email, Password, Theme
                  FROM Users
                  WHERE Email = @Email";

            using SqlCommand command =
                new SqlCommand(sql, connection);

            command.Parameters.AddWithValue(
                "@Email",
                email
            );

            using SqlDataReader reader =
                command.ExecuteReader();

            if (reader.Read())
            {
                return new User
                {
                    Id = reader.GetInt32(0),
                    Firstname = reader.GetString(1),
                    Lastname = reader.GetString(2),
                    Email = reader.GetString(3),
                    Password = reader.GetString(4),
                    Theme = reader.GetString(5)
                };
            }

            return null;
        }
    }
}