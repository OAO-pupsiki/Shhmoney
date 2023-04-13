using System.Security.Cryptography;

namespace Shhmoney.Utils
{
    public class TokenGenerator
    {
        public static string GenerateToken()
        {
            var rng = RandomNumberGenerator.Create();
            var bytes = new byte[32];
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
