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
        private readonly UserSessionRepository _userSessionRepository;

        public AuthenticationService()
        {
            _userRepository = new UserRepository();
            _userSessionRepository = new UserSessionRepository();
        }

        public bool Login(string username, string password, bool rememberMe)
        {   
            User user = _userRepository.GetUserByUsername(username);
            if (user != null && password == user.Password /*PasswordHasher.VerifyPassword(password, user.Password)*/)
            {
                if (rememberMe)
                {
                    CreateSession(user);
                }
                return true;
            }
            return false;
        }

        public void LogOut()
        {
            
        }

        /*public bool TryAutoLogin()
        {
        
        }*/

        private void CreateSession(User user)
        {
            var token = GenerateToken();
            var userSession = new UserSession
            {
                Token = token,
                Expiration = DateTime.Now.AddDays(30),
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
            byte[] bytes = Convert.FromBase64String(token);
            File.WriteAllBytes(@"D:\token.bin", bytes);
        }
    }
}
