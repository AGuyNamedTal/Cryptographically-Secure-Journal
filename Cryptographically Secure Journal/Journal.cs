using System;
using System.IO;
using System.Linq;
using System.Text;

namespace CryptographicallySecureJournal
{
    public class Journal
    {
        private static readonly byte[] SupportedVersions = { 0 };
        private const byte CurrentMajVersion = 0;
        private const ushort CurrentMinorVersion = 1;

        private static readonly byte[] Magic = Encoding.ASCII.GetBytes("JON");

        public class CorruptJournalException : Exception
        {

            public CorruptJournalException(string message) : base(message)
            {
            }
        }


        private EncryptedShare[] _encryptedShares;

        public int[] QuestionsIndexes
        {
            get
            {
                return EncryptedShares?.Select(question => (int)question.QuestionIndex).ToArray();
            }
        }
        public EncryptedShare[] EncryptedShares
        {
            get => _encryptedShares;
            set
            {
                _encryptedShares = value;
                const byte secQuestionsFlag = ((byte)JournalFlags.NoSecurityQuestions);
                if (_encryptedShares == null)
                {
                    Flags |= secQuestionsFlag;
                }
                else
                {
                    if ((Flags & secQuestionsFlag) == secQuestionsFlag)
                    {
                        Flags -= secQuestionsFlag;
                    }
                }
            }
        }
        [Flags]
        public enum JournalFlags
        {
            NoSecurityQuestions = 1 << 0,
        }

        public byte[] EncryptedText;
        public byte[] PassSalt;
        public byte VerMajor;
        public ushort VerMinor;
        public byte Flags;
        public Journal(MemoryStream memoryStream)
        {
            VerifyMagic(memoryStream);
            GetVersions(memoryStream);
            if (!SupportedVersions.Contains(VerMajor))
            {
                throw new CorruptJournalException($"Version {VerMajor} not supported");
            }
            Flags = memoryStream.Read(1)[0];
            PassSalt = memoryStream.Read(HashAndSalt.SaltLength);
            ParseSecurityQuestions(memoryStream);
            //EncryptedText = memoryStream.ToArray().Skip((int)memoryStream.Position).ToArray();
            EncryptedText = memoryStream.Read((int)(memoryStream.Length - memoryStream.Position));
        }

        public Journal(byte[] encryptedText, byte[] passSalt, EncryptedShare[] encryptedShares = null)
        {
            VerMajor = CurrentMajVersion;
            VerMinor = CurrentMinorVersion;
            EncryptedText = encryptedText;
            PassSalt = passSalt;
            EncryptedShares = encryptedShares;
        }


        public static (Journal, byte[]) GenerateNewJournal(string text, string pass, Tuple<int, string>[] securityQuestions,
            Action<int> progressBarUpdate)
        {
            byte[] salt = HashAndSalt.GenerateSalt();
            byte[] password = HashAndSalt.Password(Encoding.UTF8.GetBytes(pass), salt);
            EncryptedShare[] encryptedShares = null;
            const int progressAfterGeneration = 100;
            if (securityQuestions != null)
            {
                int startingProgress = (progressAfterGeneration / (1 + securityQuestions.Length));
                progressBarUpdate(startingProgress);
                encryptedShares = EncryptedShare.CreateSecurityQuestions(securityQuestions,
                    password, value =>
                    {
                        progressBarUpdate((int)(startingProgress + value / 100d * (progressAfterGeneration - startingProgress)));
                    });
            }
            else
            {
                progressBarUpdate(progressAfterGeneration);
            }
            Journal journal = new Journal(
                AesEncryption.Encrypt(Encoding.UTF8.GetBytes(text), password),
                salt, encryptedShares);
            return (journal, password);
        }

        public MemoryStream ToMemoryStream()
        {
            MemoryStream memoryStream = new MemoryStream();
            WriteMagic(memoryStream);
            WriteVersions(memoryStream);
            memoryStream.Write(new[] { Flags });
            memoryStream.Write(PassSalt);
            WriteSecurityQuestions(memoryStream);
            memoryStream.Write(EncryptedText);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }

        private static void WriteMagic(MemoryStream memoryStream)
        {
            memoryStream.Write(Magic);
        }

        private static void VerifyMagic(MemoryStream memoryStream)
        {
            const string errorMsg = "Invalid Magic";
            byte[] buffer = memoryStream.Read(Magic.Length);
            if (!buffer.SequenceEqual(Magic))
            {
                throw new CorruptJournalException(errorMsg);
            }
        }

        private void GetVersions(MemoryStream memoryStream)
        {
            byte[] buffer = memoryStream.Read(3);
            VerMajor = buffer[0];
            VerMinor = BitConverter.ToUInt16(buffer, 1);
        }

        private void WriteVersions(MemoryStream memoryStream)
        {
            byte[] buffer = new byte[3];
            buffer[0] = VerMajor;
            BitConverter.GetBytes(VerMinor).CopyTo(buffer, 1);
            memoryStream.Write(buffer);
        }



        private void ParseSecurityQuestions(MemoryStream memoryStream)
        {
            if ((Flags & (byte)(JournalFlags.NoSecurityQuestions)) == (byte)(JournalFlags.NoSecurityQuestions))
            {
                EncryptedShares = null;
                return;
            }
            const int securityQuestionsCount = 4;
            EncryptedShares = new EncryptedShare[securityQuestionsCount];
            for (int i = 0; i < securityQuestionsCount; i++)
            {
                EncryptedShares[i] = new EncryptedShare(memoryStream);
            }
        }

        private void WriteSecurityQuestions(MemoryStream memoryStream)
        {
            if (EncryptedShares == null)
            {
                return;
            }
            foreach (EncryptedShare securityQuestions in EncryptedShares)
            {
                securityQuestions.WriteToMemoryStream(memoryStream);
            }
        }


    }
}
