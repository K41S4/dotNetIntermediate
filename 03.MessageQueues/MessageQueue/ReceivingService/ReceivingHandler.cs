using Common;
using Common.Messages;
using RabbitMQ.Client.Events;
using System;

namespace ReceivingService
{
    public static class ReceivingHandler
    {
        private static readonly MessageService MessageService= new MessageService();

        public static void Received(object sender, BasicDeliverEventArgs args)
        {
            var messageChunk = ChunkConverter.BytesToChunk(args.Body.ToArray());

            if (MessageService.TryGetMessage(messageChunk, out var data))
            {
                var fileName = FileOperatingService.FromBytes(data, out var file);

                var filePath = $"{Common.Constants.ReceiveFolder}\\{fileName}";
                var index = 0;
                var extension = Path.GetExtension(filePath);
                while (File.Exists(filePath))
                {
                    index++;
                    var newName = $"{Path.GetFileNameWithoutExtension(filePath)}_{index}";
                    filePath = $"{Common.Constants.ReceiveFolder}\\{newName}{extension}";
                }

                FileOperatingService.WriteToFile(filePath, file);

                Console.WriteLine($"File {fileName} is received!");
            }
        }
    }
}
