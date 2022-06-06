namespace OrderManagement.API.Core.RabbitMQ.Interface
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);
    }
}
