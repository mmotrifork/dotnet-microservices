using dotnet_consumer.Model;

namespace dotnet_consumer
{
    public class PersistenceManager() : IPersistenceManager
    {
        public async Task SavePayloadAsync(dotnet_rabbitmq.Payload payload)
        {
            var localPayload = payload.ToLocal();
            using var context = new PayloadContext();
            context.Payloads.Add(localPayload);
            await context.SaveChangesAsync();
        }
    }
}