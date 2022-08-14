using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.IO;
using System.Security.Cryptography;

namespace CryptographicallySecureJournal
{
    public static class AesEncryption
    {
        public const int KeySize = 256;
        private const int MacSize = 128;
        private const int NonceSize = 128;

        private static readonly RNGCryptoServiceProvider Random = new RNGCryptoServiceProvider();

        public static byte[] Encrypt(byte[] message, byte[] key)
        {
            //User Error Checks
            CheckKey(key);

            //Using random nonce large enough not to repeat
            byte[] nonce = new byte[NonceSize / 8];
            Random.GetBytes(nonce);

            GcmBlockCipher cipher = new GcmBlockCipher(new AesEngine());
            AeadParameters parameters = new AeadParameters(new KeyParameter(key), MacSize, nonce);
            cipher.Init(true, parameters);

            //Generate Cipher Text With Auth Tag
            byte[] cipherText = new byte[cipher.GetOutputSize(message.Length)];
            int len = cipher.ProcessBytes(message, 0, message.Length, cipherText, 0);
            cipher.DoFinal(cipherText, len);

            //Assemble Message
            using (MemoryStream combinedStream = new MemoryStream())
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(combinedStream))
                {
                    //Prepend Nonce
                    binaryWriter.Write(nonce);
                    //Write Cipher Text
                    binaryWriter.Write(cipherText);
                }
                return combinedStream.ToArray();
            }
        }
        public static byte[] Decrypt(byte[] encryptedMessage, byte[] key)
        {
            //User Error Checks
            CheckKey(key);

            using (MemoryStream cipherStream = new MemoryStream(encryptedMessage))
            using (BinaryReader cipherReader = new BinaryReader(cipherStream))
            {
                //Grab Nonce
                byte[] nonce = cipherReader.ReadBytes(NonceSize / 8);

                GcmBlockCipher cipher = new GcmBlockCipher(new AesEngine());
                AeadParameters parameters = new AeadParameters(new KeyParameter(key), MacSize, nonce);
                cipher.Init(false, parameters);

                //Decrypt Cipher Text
                byte[] cipherText = cipherReader.ReadBytes(encryptedMessage.Length - nonce.Length);
                byte[] plainText = new byte[cipher.GetOutputSize(cipherText.Length)];

                int len = cipher.ProcessBytes(cipherText, 0, cipherText.Length, plainText, 0);
                cipher.DoFinal(plainText, len);

                return plainText;
            }
        }

        private static void CheckKey(byte[] key)
        {
            if (key == null || key.Length != KeySize / 8)
            {
                throw new ArgumentException($"Key needs to be {KeySize} bit! actual:{key?.Length * 8}", nameof(key));
            }
        }

    }
}
