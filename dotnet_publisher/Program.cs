using dotnet_rabbitmq;

using var messageBroker = new MessageBroker();
var message = "Hello World!";
messageBroker.Publish(message);