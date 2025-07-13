namespace PaymentService.Infrastructure.Interfaces.Producers
{
    public interface IProducer
    {
        public Task PublishAsync<T>(T message, string exchange, string routingKey);
    }
}
