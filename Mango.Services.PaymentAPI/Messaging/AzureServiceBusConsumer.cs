using System.Text;
using Azure.Messaging.ServiceBus;
using Mango.MessageBus;
using Mango.Services.PaymentAPI.Messages;
using Newtonsoft.Json;
using PaymentProcessor;

namespace Mango.Services.PaymentAPI.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string subscriptionPayment;
        private readonly string orderPaymentProcessTopic;
        private readonly string orderUpdatePaymentResultTopic;

        private readonly IConfiguration configuration;
        private readonly IMessageBus messageBus;
        private readonly IProcessPayment processPayment;

        private ServiceBusProcessor orderPaymentProcessor;

        public AzureServiceBusConsumer(IConfiguration configuration, IMessageBus messageBus, IProcessPayment processPayment)
        {
            this.configuration = configuration;
            this.messageBus = messageBus;
            this.processPayment = processPayment;

            serviceBusConnectionString = configuration.GetValue<string>("ServiceBusConnectionString");
            subscriptionPayment = configuration.GetValue<string>("OrderPaymentProcessSubscription");
            orderPaymentProcessTopic = configuration.GetValue<string>("OrderPaymentProcessTopics");
            orderUpdatePaymentResultTopic = configuration.GetValue<string>("OrderUpdatePaymentResultTopic");

            var client = new ServiceBusClient(serviceBusConnectionString);
            orderPaymentProcessor = client.CreateProcessor(orderPaymentProcessTopic, subscriptionPayment);
        }

        public async Task Start()
        {
            orderPaymentProcessor.ProcessMessageAsync += ProcessPayments;
            orderPaymentProcessor.ProcessErrorAsync += ErrorHandler;
            await orderPaymentProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            await orderPaymentProcessor.StopProcessingAsync();
            await orderPaymentProcessor.DisposeAsync();
        }

        private async Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return;
        }

        private async Task ProcessPayments(ProcessMessageEventArgs args)
        {
            ServiceBusReceivedMessage message = args.Message;

            var body = Encoding.UTF8.GetString(message.Body);
            PaymentRequestMessage paymentRequestMessage = JsonConvert.DeserializeObject<PaymentRequestMessage>(body);

            var result = processPayment.PaymentProcessor();

            UpdatePaymentResultMessage updatePaymentResultMessage = new UpdatePaymentResultMessage()
            {
                Status = result,
                OrderId = paymentRequestMessage.OrderId,
                Email = paymentRequestMessage.Email,
            };

            try
            {
                await messageBus.PublishMessage(updatePaymentResultMessage, orderUpdatePaymentResultTopic);
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
