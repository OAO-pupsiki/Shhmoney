using System.Security.Cryptography;
using System.Text;

namespace Shhmoney.Utils
{
    public class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
            byte[] hashBytes = sha256.ComputeHash(passwordBytes);
            string hashPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            return hashPassword;
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return HashPassword(password) == hashedPassword;
        }
    }
}
