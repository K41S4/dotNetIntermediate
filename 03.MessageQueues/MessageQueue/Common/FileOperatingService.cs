using System;
using System.Buffers.Binary;
using System.Text;

namespace Common
{
    public class FileOperatingService
    {
        private static byte[] ReadFromFile(string path)
        {
            using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            var bytes = new byte[fileStream.Length];
            fileStream.Read(bytes);
            return bytes;
        }

        public static void WriteToFile(string path, byte[] data)
        {
            using var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            fileStream.Write(data);
        }

        public static string FromBytes(byte[] data, out byte[] result)
        {
            var fileNameLength = BinaryPrimitives.ReadInt32BigEndian(new Span<byte>(data, 0, 4));

            var fileNameBytes = new byte[fileNameLength];
            new ReadOnlySpan<byte>(data, 4, fileNameLength).CopyTo(fileNameBytes);
            var fileName = Encoding.ASCII.GetString(fileNameBytes);

            result = new byte[data.Length - 4 - fileNameLength];
            new ReadOnlySpan<byte>(data, 4 + fileNameLength, result.Length).CopyTo(result);

            return fileName;
        }

        public static byte[] ToBytes(string path, string fileName)
        {
            var fileNameBytes = Encoding.ASCII.GetBytes(fileName);
            var fileNameLength = fileNameBytes.Length;
            var data = ReadFromFile(path);

            var result = new byte[4 + fileNameLength + data.Length];

            BinaryPrimitives.WriteInt32BigEndian(new Span<byte>(result, 0, 4), fileNameLength);
            fileNameBytes.CopyTo(new Span<byte>(result, 4, fileNameLength));
            data.CopyTo(new Span<byte>(result, 4 + fileNameLength, data.Length));

            return result;
        }
        public static void CreateDirectoryIfNeeded(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
