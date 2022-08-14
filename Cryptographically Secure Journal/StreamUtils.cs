using System.IO;

namespace CryptographicallySecureJournal
{
    public static class StreamUtils
    {
        public static byte[] Read(this Stream stream, int toRead)
        {
            byte[] buffer = new byte[toRead];
            if (stream.Read(buffer, 0, buffer.Length) < toRead)
            {
                throw new Journal.CorruptJournalException("Not enough bytes");
            }
            return buffer;
        }

        public static void Write(this Stream stream, byte[] bytes)
        {
            stream.Write(bytes, 0, bytes.Length);
        }
    }
}
