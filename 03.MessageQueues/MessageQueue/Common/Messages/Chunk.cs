using System;

namespace Common.Messages
{
    public class Chunk
    {
        public Guid Id;
        public int ChunksCount;
        public int ChunkNumber;
        public byte[] Data;
    }
}
