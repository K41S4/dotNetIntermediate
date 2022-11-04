using System;
using System.Buffers.Binary;

namespace Common.Messages
{
    public class ChunkConverter
    {
        public static byte[] ChunkToBytes(Chunk messageChunk)
        {
            var result = new byte[Constants.MetaDataSizes + messageChunk.Data.Length];

            messageChunk.Id.TryWriteBytes(new Span<byte>(result, 0, 16));
            BinaryPrimitives.WriteInt32BigEndian(new Span<byte>(result, 16, 4), messageChunk.ChunksCount);
            BinaryPrimitives.WriteInt32BigEndian(new Span<byte>(result, 20, 4), messageChunk.ChunkNumber);
            messageChunk.Data.CopyTo(new Span<byte>(result, 24, messageChunk.Data.Length));

            return result;
        }

        public static Chunk BytesToChunk(byte[] bytes)
        {
            var result = new Chunk
            {
                Id = new Guid(new Span<byte>(bytes, 0, 16)),
                ChunksCount = BinaryPrimitives.ReadInt32BigEndian(new Span<byte>(bytes, 16, 4)),
                ChunkNumber = BinaryPrimitives.ReadInt32BigEndian(new Span<byte>(bytes, 20, 4)),
                Data = new byte[bytes.Length - Constants.MetaDataSizes]
            };
            new Span<byte>(bytes, 24, result.Data.Length).CopyTo(result.Data);

            return result;
        }
    }
}
