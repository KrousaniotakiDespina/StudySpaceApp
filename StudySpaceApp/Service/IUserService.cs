using StudySpaceApp.DTO;

namespace StudySpaceApp.Service
{
    public interface IUserService
    {
        UserReadOnlyDTO? Login(UserLoginDTO loginDTO);
    }
}