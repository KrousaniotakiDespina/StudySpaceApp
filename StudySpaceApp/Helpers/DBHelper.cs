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

            return new SqlConnection(connectionString);
        }
    }
}