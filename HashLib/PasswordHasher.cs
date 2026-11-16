using System;
using System.Security.Cryptography;
using System.Text;

namespace HashLib
{
    // Hashes PW with SHA-256
    public static class PasswordHasher
    {
        // Returns the SHA-256 hash of the PW as hex string
        public static string Hash(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        // Hashes typed PW again and compares it with stored hash
        public static bool Verify(string password, string stored)
        {
            return Hash(password) == stored;
        }
    }
}
