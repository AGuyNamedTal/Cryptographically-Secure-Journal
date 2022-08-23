using CryptographicallySecureJournal.Crypto;
using CryptographicallySecureJournal.Utils;
using System;
using System.IO;
using System.Text;

namespace CryptographicallySecureJournal
{
    public class EncryptedShare
    {

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
            byte[] key, ProgressUpdater progressUpdater)
        {

            byte[][] split = ShamirSecretSharing.SplitSecret(key);
            byte[][] encryptedBytes = new byte[split.Length][];
            byte[][] salts = new byte[split.Length][];
            for (int i = 0; i < split.Length; i++)
            {
                byte[] message = Encoding.UTF8.GetBytes(securityQuestions[i].Item2);
                byte[] salt = HashAndSalt.GenerateSalt();
                salts[i] = salt;
                byte[] shareKey = HashAndSalt.SecurityAnswer(message, salt);
                encryptedBytes[i] = AesEncryption.Encrypt(split[i], shareKey);
                progressUpdater.Update((double)(i + 1) / split.Length);
            }
            EncryptedShare[] output = new EncryptedShare[split.Length];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = new EncryptedShare((byte)securityQuestions[i].Item1,
                    encryptedBytes[i], salts[i]);
            }
            return output;
        }

        public static byte[] RecoverKey(Tuple<int, string>[] securityQuestions, EncryptedShare[] encryptedShares,
            ProgressUpdater progressUpdater)
        {
            (int, byte[])[] shares = new (int, byte[])[securityQuestions.Length];
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
                shares[currentShareIndex] = (shareX, decryptedShare);
                currentShareIndex++;
                progressUpdater.Update(0.9d * currentShareIndex / shares.Length);
            }

            byte[] key = ShamirSecretSharing.ReconstructSecret(shares);
            progressUpdater.Update(1);
            return key;
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
