using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using SocialMedia.Application.Common.Interfaces;
using System.Security.Cryptography;

namespace SocialMedia.Infrastructure.Utilities
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int Saltsize = 128 / 8;
        private const int Keysize = 256 / 8;
        private const int Iteration = 10000;

        public string Hash(string password)
        {
            // Generate Salt
            byte[] salt = new byte[Saltsize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Get subkey
            byte[] subkey = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, Iteration, Keysize);


            // Combine salt and subkey
            byte[] output = new byte[Saltsize + Keysize];
            Buffer.BlockCopy(salt, 0, output, 0, Saltsize);
            Buffer.BlockCopy(subkey, 0, output, Saltsize, Keysize);

            return Convert.ToBase64String(output);
        }

        public bool Verify(string inputPassword, string hashedPassword)
        {
            var decodedHashedPassword = Convert.FromBase64String(hashedPassword);

            // Extract the salt and subkey from the hashed password
            byte[] salt = new byte[Saltsize];
            Buffer.BlockCopy(decodedHashedPassword, 0, salt, 0, Saltsize);
            byte[] storedSubkey = new byte[Keysize];
            Buffer.BlockCopy(decodedHashedPassword, Saltsize, storedSubkey, 0, Keysize);

            // Get subkey for input password
            byte[] generatedSubkey = KeyDerivation.Pbkdf2(inputPassword, salt, KeyDerivationPrf.HMACSHA256, Iteration, Keysize);

            return CryptographicOperations.FixedTimeEquals(storedSubkey, generatedSubkey);
        }
    }
}
