using Microsoft.AspNetCore.Identity;
using StudySpaceApp.DAO;
using StudySpaceApp.DTO;
using StudySpaceApp.Models;

namespace StudySpaceApp.Service
{
    public class UserServiceImpl : IUserService
    {
        private readonly IUserDAO _userDAO;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserServiceImpl(
            IUserDAO userDAO,
            IPasswordHasher<User> passwordHasher)
        {
            _userDAO = userDAO;
            _passwordHasher = passwordHasher;
        }

        public UserReadOnlyDTO? Login(UserLoginDTO loginDTO)
        {
            User? user = _userDAO.GetUserByEmail(loginDTO.Email);

            if (user == null)
            {
                return null;
            }

            PasswordVerificationResult result =
                _passwordHasher.VerifyHashedPassword(
                    user,
                    user.Password,
                    loginDTO.Password
                );

            if (result == PasswordVerificationResult.Failed)
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