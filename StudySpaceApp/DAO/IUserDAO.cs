using StudySpaceApp.Models;

namespace StudySpaceApp.DAO
{
    public interface IUserDAO
    {
        User? GetUserByEmailAndPassword(string email, string password);
    }
}