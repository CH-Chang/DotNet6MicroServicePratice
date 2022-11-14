namespace Mango.MessageBus
{
    using System.Text;
    using Azure.Messaging.ServiceBus;
    using Newtonsoft.Json;

    /// <summary>
    /// Azure Enent Bus 實作
    /// </summary>
    public class AzureServiceBusMessageBus : IMessageBus
    {
        // 教程使用的連線字串
        private readonly string connectionString = "Endpoint=sb://mangorestaurant.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=7t3usJ6tooj30PRi5ccrTwbxS6DNGPxbmp2oVdiO3cI=";

        /// <inheritdoc />
        public async Task PublishMessage(BaseMessage message, string topic)
        {
            await using var client = new ServiceBusClient(this.connectionString);
            ServiceBusSender sender = client.CreateSender(topic);

            string messageJson = JsonConvert.SerializeObject(message);
            byte[] messageBytes = Encoding.UTF8.GetBytes(messageJson);
            ServiceBusMessage messageEvent = new ServiceBusMessage(messageBytes) { CorrelationId = Guid.NewGuid().ToString() };

            await sender.SendMessageAsync(messageEvent);
            await client.DisposeAsync();
        }
    }
}
