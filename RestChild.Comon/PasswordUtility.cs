using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RestChild.Comon
{
    public static class PasswordUtility
    {
        private const int SaltStrength = 10;
        private static readonly MD5 Hasher = MD5.Create();
        private static readonly Random RandomProvider = new Random();

        private static readonly char[] ValidChars = Enumerable.Range('0', 10)
            .Concat(Enumerable.Range('a', 'z' - 'a' + 1))
            .Select(item => (char) item)
            .ToArray();

        public static byte[] GetPasswordHash(string password, byte[] salt)
        {
            var hash = Encoding.UTF8.GetBytes(password).Concat(salt).ToArray();
            hash = Hasher.ComputeHash(hash);

            return hash;
        }

        public static byte[] GenerateSalt()
        {
            var result = new byte[SaltStrength];
            RandomProvider.NextBytes(result);

            return result;
        }

        public static string GeneratePassword(int length)
        {
            var builder = new StringBuilder(length);

            for (var i = 0; i < length; i++)
            {
                var index = RandomProvider.Next(ValidChars.Length);
                builder.Append(ValidChars[index]);
            }

            var result = builder.ToString();

            return result;
        }
    }
}
