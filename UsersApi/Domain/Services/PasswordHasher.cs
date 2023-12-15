using System.Security.Cryptography;
using System.Text;

namespace Domain.Services
{
    public static class PasswordHasher
    {
        public static string GenerateHash(string password, string salt)
        {
            string hashWithSalt = password + salt;
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(hashWithSalt);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }

            return Convert.ToBase64String(saltBytes);
        }

    }
}