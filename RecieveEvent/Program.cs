using System;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using System.Threading.Tasks;

namespace RecieveEvent
{
    public class Program
    {
        private const string EventHubConnectionString = "Endpoint=sb://arena1.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=K/RG97afoQofAWrKQ9vvjJuOVzCzxp+zDChUrNm+lJ4=";
        private const string EventHubName = "arenaeventhub";
        private const string StorageContainerName = "arenaeventcontainer";
        private const string StorageAccountName = "arenaeventstorage";
        private const string StorageAccountKey = "p0NLPtk7Gm6M9LjDmphdIDaxyceUEeIQmmtSgqETwZU3cMvsFMkKL5U1yUObV1sXU3LETDybbmF69xdTIREkxQ==";

        private static readonly string StorageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", StorageAccountName, StorageAccountKey);

        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            Console.WriteLine("Registering EventProcessor...");

            var eventProcessorHost = new EventProcessorHost(
                EventHubName,
                PartitionReceiver.DefaultConsumerGroupName,
                EventHubConnectionString,
                StorageConnectionString,
                StorageContainerName);

            // Registers the Event Processor Host and starts receiving messages
            await eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>();

            Console.WriteLine("Receiving. Press ENTER to stop worker.");
            Console.ReadLine();

            // Disposes of the Event Processor Host
            await eventProcessorHost.UnregisterEventProcessorAsync();
        }
    }
}
