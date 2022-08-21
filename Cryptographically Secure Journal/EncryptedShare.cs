using CryptographicallySecureJournal.Utils;
using SecretSharingDotNet.Cryptography;
using SecretSharingDotNet.Math;
using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace CryptographicallySecureJournal
{
    public class EncryptedShare
    {
        public const int MinimumNumberOfQuestions = 3;

        public byte QuestionIndex;
        public byte[] Share;
        public byte[] Salt;

        public EncryptedShare(byte questionIndex, byte[] share, byte[] salt)
        {
            QuestionIndex = questionIndex;
            Share = share;
            Salt = salt;
        }

        public EncryptedShare(MemoryStream memoryStream)
        {
            QuestionIndex = memoryStream.Read(1)[0];
            Salt = memoryStream.Read(HashAndSalt.SaltLength);
            int answerLength = BitConverter.ToInt32(memoryStream.Read(sizeof(int)), 0);
            Share = memoryStream.Read(answerLength);
        }

        public static EncryptedShare[] CreateSecurityQuestions(Tuple<int, string>[] securityQuestions,
            byte[] key, Action<int> progressBarUpdater)
        {

            byte[][] split = SplitKey(key);
            byte[][] encryptedBytes = new byte[split.Length][];
            byte[][] salts = new byte[split.Length][];
            for (int i = 0; i < split.Length; i++)
            {
                byte[] message = Encoding.UTF8.GetBytes(securityQuestions[i].Item2);
                byte[] salt = HashAndSalt.GenerateSalt();
                salts[i] = salt;
                byte[] shareKey = HashAndSalt.SecurityAnswer(message, salt);
                encryptedBytes[i] = AesEncryption.Encrypt(split[i], shareKey);
                progressBarUpdater((i + 1) * 100 / split.Length);
            }
            EncryptedShare[] output = new EncryptedShare[split.Length];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = new EncryptedShare((byte)securityQuestions[i].Item1,
                    encryptedBytes[i], salts[i]);
            }
            return output;
        }

        public static byte[] RecoverKey(Tuple<int, string>[] securityQuestions, EncryptedShare[] encryptedShares, Action<int> progressBar)
        {
            ExtendedEuclideanAlgorithm<BigInteger> gcd = new ExtendedEuclideanAlgorithm<BigInteger>();
            ShamirsSecretSharing<BigInteger> combine = new ShamirsSecretSharing<BigInteger>(gcd);
            FinitePoint<BigInteger>[] shares = new FinitePoint<BigInteger>[securityQuestions.Length];
            int currentShareIndex = 0;
            foreach (Tuple<int, string> tuple in securityQuestions)
            {
                (int questionIndex, string answer) = tuple;
                EncryptedShare matchingShare = null;
                int shareX = -1;
                for (int shareIndex = 0; shareIndex < encryptedShares.Length; shareIndex++)
                {
                    EncryptedShare share = encryptedShares[shareIndex];
                    if (share.QuestionIndex == questionIndex)
                    {
                        matchingShare = share;
                        shareX = shareIndex + 1;
                        break;
                    }
                }
                if (matchingShare == null)
                {
                    throw new ArgumentException($"Question number {questionIndex} not found in journal");
                }

                byte[] decryptedShare = AesEncryption.Decrypt(matchingShare.Share,
                    HashAndSalt.SecurityAnswer(Encoding.UTF8.GetBytes(answer), matchingShare.Salt));
                shares[currentShareIndex] = new FinitePoint<BigInteger>(
                    new BigIntCalculator(new BigInteger(shareX)),
                    new BigIntCalculator(decryptedShare));
                currentShareIndex++;
                progressBar(90 * shares.Length / currentShareIndex);
            }

            byte[] key = combine.Reconstruction(shares).ToByteArray();

            progressBar(100);
            return key;
        }


        private static byte[][] SplitKey(byte[] key)
        {
            ExtendedEuclideanAlgorithm<BigInteger> gcd = new ExtendedEuclideanAlgorithm<BigInteger>();
            //// Create Shamir's Secret Sharing instance with BigInteger
            ShamirsSecretSharing<BigInteger> split = new ShamirsSecretSharing<BigInteger>(gcd);

            //// Minimum number of shared secrets for reconstruction: 4
            //// Maximum number of shared secrets: 10
            //// Attention: The password length changes the security level set by the ctor
            Shares<BigInteger> shares = split.MakeShares(MinimumNumberOfQuestions, 4,
                key, 15);

            return shares.Select(point => point.Y.ByteRepresentation.ToArray()).ToArray();
        }

        public void WriteToMemoryStream(MemoryStream memoryStream)
        {
            memoryStream.Write(new[] { QuestionIndex });
            memoryStream.Write(Salt);
            byte[] answer = Share;
            byte[] answerLength = BitConverter.GetBytes(answer.Length);
            memoryStream.Write(answerLength);
            memoryStream.Write(answer);
        }

    }
}
