using dotnet_rabbitmq;

using var messageBroker = new MessageBroker();

for (int i = 0; i < 25; i++)
{
    var payload = new Payload(DateTime.UtcNow, i);
    var message = payload.ToJson();
    messageBroker.Publish(message);
    var delay = new Random().Next(100, 500);
    Thread.Sleep(delay);
}