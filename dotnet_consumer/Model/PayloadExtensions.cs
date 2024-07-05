namespace dotnet_consumer.Model;

public static class PayloadExtensions
{
    public static Payload ToLocal(this dotnet_rabbitmq.Payload payload)
    {
        return new Payload
        {
            TimeStamp = payload.TimeStamp,
            Count = payload.Count
        };
    }
    
    public static dotnet_rabbitmq.Payload ToPayload(this string json)
    {
        return new dotnet_rabbitmq.Payload(json);
    }
}