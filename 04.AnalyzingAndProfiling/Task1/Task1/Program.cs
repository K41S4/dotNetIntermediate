using System.Security.Cryptography;

namespace Task1
{
    internal class Program
    {
        private static byte[] Salt = new byte[16] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        private const string Password = "Password!.";

        static void Main(string[] args)
        {
            var result = GeneratePasswordHashUsingSalt(Password, Salt);
            Console.WriteLine(result);
        }

        public static string GeneratePasswordHashUsingSalt(string passwordText, byte[] salt)
        {
            var iterate = 10000;

            var hash = new Rfc2898DeriveBytes(passwordText, salt, iterate, HashAlgorithmName.SHA256).GetBytes(20);

            Span<byte> hashBytes = stackalloc byte[36];

            salt.CopyTo(hashBytes[0..16]);
            hash.CopyTo(hashBytes[16..36]);

            var passwordHash = Convert.ToBase64String(hashBytes);

            return passwordHash;
        }
    }
}
