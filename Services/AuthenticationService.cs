using Shhmoney.Utils;
using Shhmoney.Data;
using Shhmoney.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;

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

        public bool Login(string username, string password, bool rememberMe)
        {   
            User user = _userRepository.GetUserByUsername(username);
            if (user != null && PasswordHasher.VerifyPassword(password, user.Password))
            {
                var session = _userSessionRepository.GetSessionByUser(user);
                if (rememberMe)
                {
                    UpdateSession(user);
                }
                return true;
            }
            return false;
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
            if (session.User == null)
                return false;

            return session.Expiration > DateTime.UtcNow;
        }

        public bool SignUp(string username, string password, string email)
        {
            if (_userRepository.GetUserByUsername(username) != null)
                throw new Exception("username is already taken");

            var role = _roleRepository.GetRoleByName("User");
 

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

        private void UpdateSession(User user)
        {
            var session = _userSessionRepository.GetSessionByUser(user);
            _userSessionRepository.UpdateSession(session);
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
