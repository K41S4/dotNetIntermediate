using System;
using System.Collections.Concurrent;

namespace Common.Messages
{
    public class MessageService
    {
        private readonly ConcurrentDictionary<Guid, List<Chunk>> chunksDictionary;

        public MessageService()
        {
            chunksDictionary = new ConcurrentDictionary<Guid, List<Chunk>>();
        }

        public static List<Chunk> GetMessageChunks(byte[] data)
        {
            var result = new List<Chunk>();

            var messageId = Guid.NewGuid();

            if (data.Length <= Constants.MaxMessageSize)
            {
                result.Add(new Chunk
                {
                    Id = messageId,
                    ChunkNumber = 0,
                    ChunksCount = 1,
                    Data = data
                });
            }
            else
            {
                var chunksCount = (data.Length - 1) / Constants.MaxMessageSize + 1;
                for (var i = 0; i < chunksCount; i++)
                {
                    var chunkSize = i != chunksCount - 1 ? Constants.MaxMessageSize : data.Length % Constants.MaxMessageSize;
                    var chunkData = new byte[chunkSize];

                    Array.Copy(data, Constants.MaxMessageSize * i, chunkData, 0, chunkSize);

                    var messageChunk = new Chunk
                    {
                        Id = messageId,
                        ChunkNumber = i,
                        ChunksCount = chunksCount,
                        Data = chunkData
                    };

                    result.Add(messageChunk);
                }
            }

            return result;
        }

        public bool TryGetMessage(Chunk messageChunk, out byte[] data)
        {
            if (messageChunk.ChunksCount == 1)
            {
                data = new byte[messageChunk.Data.Length];
                messageChunk.Data.CopyTo(data, 0);
                return true;
            }

            var allChunks = chunksDictionary.GetOrAdd(messageChunk.Id, new List<Chunk>());
            allChunks.Add(messageChunk);

            if (allChunks.Count != messageChunk.ChunksCount)
            {
                data = null;
                return false;
            }

            allChunks.Sort((x, y) => x.ChunkNumber.CompareTo(y.ChunkNumber));
            var result = new List<byte>();
            for (var i = 0; i < allChunks.Count; i++)
            {
                result.AddRange(allChunks[i].Data);
            }

            data = result.ToArray();
            return true;
        }
    }
}
