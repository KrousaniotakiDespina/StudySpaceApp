using StudySpaceApp.DAO;
using StudySpaceApp.DTO;
using StudySpaceApp.Models;

namespace StudySpaceApp.Service
{
    public class UserServiceImpl : IUserService
    {
        private readonly IUserDAO _userDAO;

        public UserServiceImpl(IUserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        public UserReadOnlyDTO? Login(UserLoginDTO loginDTO)
        {
            User? user = _userDAO.GetUserByEmail(loginDTO.Email);

            if (user == null)
            {
                return null;
            }

            if (user.Password != loginDTO.Password)
            {
                return null;
            }

            return new UserReadOnlyDTO
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Theme = user.Theme
            };
        }
    }
}