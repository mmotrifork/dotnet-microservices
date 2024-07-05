using dotnet_consumer.Model;

namespace dotnet_consumer
{
    public interface IPersistenceManager
    {
        Task SavePayloadAsync(dotnet_rabbitmq.Payload payload);
    }
}