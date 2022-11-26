using System.Text;
using Mango.MessageBus;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Mango.Services.PaymentAPI.RabbitMQSender
{
    public class RabbitMQPaymentMessageSender : IRabbitMQPaymentMessageSender
    {
        private readonly string hostname;
        private readonly string password;
        private readonly string username;
        private const string ExchangeName = "DirectPaymentUpdate_Exchange";
        private const string PaymentEmailUpdateQueueName = "PaymentEmailUpdateQueueName";
        private const string PaymentOrderUpdateQueueName = "PaymentOrderUpdateQueueName";

        private IConnection connection;

        public RabbitMQPaymentMessageSender()
        {
            hostname = "localhost";
            password = "guest";
            username = "guest";
        }

        public void SendMessage(BaseMessage message)
        {
            if (ConnectionExists())
            {
                using var channel = connection.CreateModel();
                channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct, durable: false);
                channel.QueueDeclare(PaymentOrderUpdateQueueName, false, false, false, null);
                channel.QueueDeclare(PaymentEmailUpdateQueueName, false, false, false, null);
                channel.QueueBind(PaymentOrderUpdateQueueName, ExchangeName, "PaymentOrder");
                channel.QueueBind(PaymentEmailUpdateQueueName, ExchangeName, "PaymentEmail");
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish(exchange: ExchangeName, "PaymentEmail", basicProperties: null, body: body);
                channel.BasicPublish(exchange: ExchangeName, "PaymentOrder", basicProperties: null, body: body);
            }
        }

        private void CreateConnection ()
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = hostname,
                    UserName = username,
                    Password = password
                };

                connection = factory.CreateConnection();
            } catch (Exception)
            {

            }
        }

        private bool ConnectionExists ()
        {
            if (connection != null)
            {
                return true;
            }

            CreateConnection();

            return connection != null;
        }
    }
}
