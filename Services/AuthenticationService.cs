using Shhmoney.Utils;
using Shhmoney.Data;
using Shhmoney.Models;

namespace Shhmoney.Services
{
    public class AuthenticationService
    {
        private readonly UserRepository _userRepository;

        public AuthenticationService()
        {
            _userRepository = new UserRepository();
        }

        public bool Login(string username, string password)
        {
            User user = _userRepository.GetUserByUsername(username);
            if (user != null && PasswordHasher.VerifyPassword(password, user.Password))
            {
                CreateSession(user);
                return true;
            }
            return false;
        }

        private void CreateSession(User user)
        {
            // Create a session for the user
        }
    }
}
