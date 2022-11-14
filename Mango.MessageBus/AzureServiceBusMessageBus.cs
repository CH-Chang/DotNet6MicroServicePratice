namespace Mango.MessageBus
{
    using System.Text;
    using Microsoft.Azure.ServiceBus;
    using Microsoft.Azure.ServiceBus.Core;
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
            ISenderClient senderClient = new TopicClient(this.connectionString, topic);

            string messageJson = JsonConvert.SerializeObject(message);
            byte[] messageBytes = Encoding.UTF8.GetBytes(messageJson);
            Message messageEvent = new Message(messageBytes) { CorrelationId = Guid.NewGuid().ToString() };

            await senderClient.SendAsync(messageEvent);

            await senderClient.CloseAsync();
        }
    }
}
