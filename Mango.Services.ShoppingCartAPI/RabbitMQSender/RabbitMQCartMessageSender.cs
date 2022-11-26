using System.Text;
using Mango.MessageBus;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Mango.Services.ShoppingCartAPI.RabbitMQSender
{
    public class RabbitMQCartMessageSender : IRabbitMQCartMessageSender
    {
        private readonly string hostname;
        private readonly string password;
        private readonly string username;

        private IConnection connection;

        public RabbitMQCartMessageSender()
        {
            hostname = "localhost";
            password = "guest";
            username = "guest";
        }

        public void SendMessage(BaseMessage message, string queueName)
        {
            if (ConnectionExists())
            {
                using var channel = connection.CreateModel();
                channel.QueueDeclare(queue: queueName, false, false, false, arguments: null);
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
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
