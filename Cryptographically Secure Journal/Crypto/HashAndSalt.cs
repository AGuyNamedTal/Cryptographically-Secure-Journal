using Isopoh.Cryptography.Argon2;
using System;
using System.Security.Cryptography;

namespace CryptographicallySecureJournal.Crypto
{
    internal static class HashAndSalt
    {
        public const int SaltLength = 24;
        private static readonly Argon2Config PasswordConfig = new Argon2Config
        {
            ClearPassword = true,
            TimeCost = 4
        };
        private static readonly Argon2Config SecurityAnswerConfig = new Argon2Config()
        {
            ClearPassword = true,
            TimeCost = 3,
            HashLength = AesEncryption.KeySize / 8
        };
        private static readonly RNGCryptoServiceProvider RNG = new RNGCryptoServiceProvider();

        public static byte[] GenerateSalt()
        {
            byte[] salt = new byte[SaltLength];
            RNG.GetBytes(salt);
            return salt;
        }

        public static byte[] Password(byte[] passwordBytes, byte[] salt)
        {
            return Argon2Hash(PasswordConfig, passwordBytes, salt);
        }
        public static byte[] SecurityAnswer(byte[] answerBytes, byte[] salt)
        {
            return Argon2Hash(SecurityAnswerConfig, answerBytes, salt);
        }
        private static byte[] Argon2Hash(Argon2Config config, byte[] data, byte[] salt)
        {
            config.Salt = salt;
            config.Password = data;
            string[] hashSplit = Argon2.Hash(config).Split('$');
            string hashStr = hashSplit[hashSplit.Length - 1];
            int requiredPadding = 4 - hashStr.Length % 4;
            if (requiredPadding > 0)
            {
                hashStr += new string('=', requiredPadding);
            }
            byte[] hash = Convert.FromBase64String(hashStr);
            return hash;
        }
    }
}
