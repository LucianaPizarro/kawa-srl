using System;
using System.Security.Cryptography;

namespace KawaSRL
{
    public static class PasswordHelper
    {
        private const int Iterations = 100000;
        private const int SaltSize = 16;
        private const int HashSize = 32;

        public static string HashPassword(string password)
        {
            byte[] salt = new byte[SaltSize];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(
                password, salt, Iterations,
                HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);

                return Iterations + "." +
                    Convert.ToBase64String(salt) + "." +
                    Convert.ToBase64String(hash);
            }
        }

        public static bool VerifyPassword(
            string password, string storedHash)
        {
            try
            {
                string[] parts = storedHash.Split('.');

                int iterations = int.Parse(parts[0]);
                byte[] salt = Convert.FromBase64String(parts[1]);
                byte[] originalHash = Convert.FromBase64String(parts[2]);

                using (var pbkdf2 = new Rfc2898DeriveBytes(
                    password, salt, iterations,
                    HashAlgorithmName.SHA256))
                {
                    byte[] newHash = pbkdf2.GetBytes(originalHash.Length);

                    int diferencia = newHash.Length ^ originalHash.Length;

                    for (int i = 0; i < newHash.Length && i < originalHash.Length; i++)
                    {
                        diferencia |= newHash[i] ^ originalHash[i];
                    }

                    return diferencia == 0;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}