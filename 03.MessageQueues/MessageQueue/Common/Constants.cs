namespace Common
{
    public class Constants
    {
        public const string Host = "localhost";
        public const string QueueName = "MessageExchange";
        public static string FileFormat = ".pdf";
        public static string InputFolder = "InputFolder";
        public static string ReceiveFolder = "ReceiveFolder";
        public const int MaxMessageSize = 8 * 1024;
        public const int MetaDataSizes = 16 + 4 + 4;
    }
}