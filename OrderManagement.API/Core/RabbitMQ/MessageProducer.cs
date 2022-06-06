using Newtonsoft.Json;
using OrderManagement.API.Core.RabbitMQ.Interface;
using RabbitMQ.Client;
using System.Text;

namespace OrderManagement.API.Core.RabbitMQ
{
    public class MessageProducer : IMessageProducer
    {
        private IConnection _connection;
        private readonly ILogger<MessageProducer> _logger;
        private readonly string _uri;

        public MessageProducer(IConfiguration configuration, ILogger<MessageProducer> logger)
        {
            _uri = configuration.GetValue<string>("RabbitMQ:ConnectionUri");
            _logger = logger;
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    Uri = new Uri(_uri)
                };

                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not create RabbitMQ connection. Detail: {ex.Message}");
            }
        }

        public void SendMessage<T>(T message)
        {
            if (IsConnected())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueDeclare("order-status-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

                    var json = JsonConvert.SerializeObject(message);
                    var body = Encoding.UTF8.GetBytes(json);

                    channel.BasicPublish(exchange: "", routingKey: "order-status-queue", body: body);
                }
            }
        }

        private bool IsConnected()
        {
            if (_connection != null)
                return true;

            CreateConnection();
            return _connection != null;
        }
    }
}
