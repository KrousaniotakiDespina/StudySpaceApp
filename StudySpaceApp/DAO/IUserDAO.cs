using StudySpaceApp.Models;

namespace StudySpaceApp.DAO
{
    public interface IUserDAO
    {
        User? GetUserByEmail(string email);
    }
}