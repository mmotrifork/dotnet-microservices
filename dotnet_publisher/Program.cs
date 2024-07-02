using dotnet_rabbitmq;

using var messageBroker = new MessageBroker();

var payload = new Payload(DateTime.Now, 42);
var message = payload.ToJson();
messageBroker.Publish(message);