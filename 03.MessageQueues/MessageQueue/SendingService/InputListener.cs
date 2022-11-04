using Common;
using Common.Messages;
using RabbitMQ.Client;

namespace SendingService
{
    public class InputListener: IDisposable
    {
        private const int ThreadTimeout = 3 * 1000;
        private const int MaxFileReadAttempts = 10;
        private readonly FileSystemWatcher fileSystemWatcher;
        private readonly string searchedExtension;
        private readonly IModel model;

        public InputListener(IModel channel, string extension)
        {
            searchedExtension = extension;
            model = channel;

            FileOperatingService.CreateDirectoryIfNeeded(Common.Constants.InputFolder);
            
            fileSystemWatcher = new FileSystemWatcher(Common.Constants.InputFolder)
            {
                EnableRaisingEvents = true
            };
            fileSystemWatcher.Created += OnCreated;
        }


        private void OnCreated(object source, FileSystemEventArgs e)
        {
            var fileExtension = Path.GetExtension(e.FullPath);
            if (searchedExtension != fileExtension)
                return;

            Console.WriteLine($"File {e.Name} is caught!");

            Task.Run(() =>
            {
                var currentRetry = 0;
                while (currentRetry < MaxFileReadAttempts)
                {
                    try
                    {
                        var data = FileOperatingService.ToBytes(e.FullPath, e.Name);

                        var messageChunks = MessageService.GetMessageChunks(data);
                        foreach (var messageChunk in messageChunks)
                        {
                            model.BasicPublish(Common.Constants.QueueName, "", null, ChunkConverter.ChunkToBytes(messageChunk));
                        }

                        Console.WriteLine($"File {e.Name} is published!");

                        return;
                    }
                    catch
                    {
                        Thread.Sleep(ThreadTimeout);
                        currentRetry++;
                    }
                }
            });
        }

        public void Dispose()
        {
            fileSystemWatcher.Dispose();
        }
    }
}
