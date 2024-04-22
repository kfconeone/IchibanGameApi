using System;
using System.Security.Cryptography;
using System.Text;


namespace IChibanGameServer
{
    public class HashManager
    {
        public static string GenerateSHA256String(string inputString)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", String.Empty);
        }


    }
}
