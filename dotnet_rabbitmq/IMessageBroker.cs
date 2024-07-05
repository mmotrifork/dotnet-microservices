namespace dotnet_rabbitmq;

public interface IMessageBroker : IDisposable
{
    void Publish(string message);
    event Action<string, Action> MessageReceived;
}
