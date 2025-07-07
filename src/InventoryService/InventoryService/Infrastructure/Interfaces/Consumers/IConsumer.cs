namespace InventoryService.Infrastructure.Interfaces.Consumers
{
    public interface IConsumer
    {
        public string QueueName { get; }
    }
}
