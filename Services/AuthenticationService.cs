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

        public AuthenticationService()
        {
            _userRepository = new UserRepository();
            _roleRepository = new RoleRepository();
            _userSessionRepository = new UserSessionRepository();
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
                    throw new Exception("Unable to load user session");

                _userSessionRepository.UpdateSession(session);
                SaveTokenToFile(session.Token);
            }
         }

        public void LogOut(User user)
        {
            var session = _userSessionRepository.GetSessionByUser(user);
            session.Expiration = DateTime.UtcNow;
        }

        public bool TryAutoLogin()
        {
            string path = @"C:\ProgramData\Shhmoney\data.bin";

            if(!File.Exists(path))
                return false;

            byte[] bytes = File.ReadAllBytes(path);
            string token = Convert.ToBase64String(bytes);
            var session = _userSessionRepository.GetSessionByToken(token);

            if (session == null)
                throw new Exception("Unable to load user session");

            if (session.Expiration > DateTime.UtcNow)
            {
                var user = session.User;
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

        private void SaveTokenToFile(string token)
        {
            string path = @"C:\ProgramData\Shhmoney";
            Directory.CreateDirectory(path);
            byte[] bytes = Convert.FromBase64String(token);

            using (var fstream = new FileStream(path + @"\data.bin", FileMode.OpenOrCreate))
            {
                fstream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
