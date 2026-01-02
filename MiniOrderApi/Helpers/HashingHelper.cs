using System.Security.Cryptography;
using System.Text;

namespace MiniOrderApi.Helpers
{
    public static class HashingHelper
    {
        public static string HashPassword(string plainPassword)
        {
            using var sha256 = SHA256.Create();

            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));

            var builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2")); 
            }

            return builder.ToString();
        }
    }
}