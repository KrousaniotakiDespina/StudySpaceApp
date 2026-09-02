using Microsoft.Data.SqlClient;

namespace StudySpaceApp.Helpers
{
    public class DBHelper
    {
        private readonly IConfiguration _configuration;

        public DBHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection GetConnection()
        {
            string? connectionString =
                _configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "Database connection string was not found."
                );
            }

            return new SqlConnection(connectionString);
        }
    }
}