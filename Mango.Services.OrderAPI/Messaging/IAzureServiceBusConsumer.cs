namespace Mango.Services.OrderAPI.Messaging
{
    public interface IAzureServiceBusConsumer
    {
        public Task Start();
        public Task Stop();
    }
}
