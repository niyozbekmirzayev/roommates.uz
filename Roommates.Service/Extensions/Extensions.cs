using System.Security.Cryptography;
using System.Text;

namespace Roommates.Service.Extensions
{
    public static class Extensions
    {
        public static string ToSHA256(this string text) 
        {
            using (var sha256 = SHA256.Create())
            {
                // Convert the input string to a byte array
                byte[] inputBytes = Encoding.UTF8.GetBytes(text);

                // Compute the hash of the input byte array
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                // Convert the hash byte array to a string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
