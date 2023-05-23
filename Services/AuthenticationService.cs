using Shhmoney.Utils;
using Shhmoney.Data;
using Shhmoney.Models;
using System.Security.Cryptography;

namespace Shhmoney.Services
{
    public class AuthenticationService
    {
        private readonly UserRepository _userRepository;
        private readonly RoleRepository _roleRepository;
        private readonly UserSessionRepository _userSessionRepository;

        public AuthenticationService(UserRepository userRepository, RoleRepository roleRepository, UserSessionRepository userSessionRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userSessionRepository = userSessionRepository;
        }

        public void Login(string username, string password, bool rememberMe)
        {   
            User user = _userRepository.GetUserByUsername(username);

            if (user == null)
                throw new Exception("Wrong username");

            if (!PasswordHasher.VerifyPassword(password, user.Password))
                throw new Exception("Wrong password");

            Utils.AppContext.CurrentUser = user;

            if (rememberMe)
            {
                var session = _userSessionRepository.GetSessionByUser(user);

                if (session == null)
                {
                    var token = GenerateToken();
                    session = new UserSession
                    {
                        Token = token,
                        Expiration = DateTime.UtcNow.AddDays(30),
                        User = user
                    };
                    _userSessionRepository.AddSession(session);
                }

                _userSessionRepository.UpdateSession(session);
                SaveTokenToFile(session.Token);
            }
         }

        public void LogOut()
        {
            var session = _userSessionRepository.GetSessionByUser(Utils.AppContext.CurrentUser);
            //session.Expiration = DateTime.UtcNow;
            if (session != null)
            {
                _userSessionRepository.RemoveSessionByToken(session.Token);
                var filePath = Path.Combine(FileSystem.AppDataDirectory, "data.bin");
                File.Delete(filePath);
            }
        }

        public bool TryAutoLogin()
        {
            string token;
            var filePath = Path.Combine(FileSystem.AppDataDirectory, "data.bin");
            if (!File.Exists(filePath)) return false;
            using (var reader = new StreamReader(filePath))
            {
                token = reader.ReadToEnd();
            }

            var session = _userSessionRepository.GetSessionByToken(token);

            if (session == null)
                return false;

            if (session.Expiration > DateTime.UtcNow)
            {

                var user = _userRepository.GetUserById(session.UserId);
                Utils.AppContext.CurrentUser = user;
                return true;
            }

            return false;
        }

        public bool SignUp(string username, string password, string email)
        {
            if (_userRepository.GetUserByUsername(username) != null)
                throw new Exception("Username is already taken");

            var role = _roleRepository.GetRoleByName("User");
            role ??= new Role
            {
                Name = "User",
                Description = "Simple user role"
            };

            var hashedPassword = PasswordHasher.HashPassword(password);
            var user = new User
            {
                Username = username,
                Password = hashedPassword,
                Email = email,
                Role = role
            };

            _userRepository.AddUser(user);
            CreateSession(user);
            return true;
        }

        private void CreateSession(User user)
        {
            var token = GenerateToken();
            var userSession = new UserSession
            {
                Token = token,
                Expiration = DateTime.UtcNow,
                User = user
            };
            _userSessionRepository.AddSession(userSession);
            SaveTokenToFile(token);
        }

        private string GenerateToken()
        {
            var rng = RandomNumberGenerator.Create();
            var bytes = new byte[32];
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        private async void SaveTokenToFile(string token)
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, "data.bin");
            using (var writer = new StreamWriter(filePath))
            {
                await writer.WriteAsync(token);
            }
        }
    }
}
