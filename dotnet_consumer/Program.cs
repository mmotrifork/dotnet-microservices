using dotnet_rabbitmq;

using var messageBroker = new MessageBroker();
messageBroker.MessageReceived += message => Console.WriteLine($"[x] Received {message}");

Console.WriteLine("Press [enter] to exit.");
Console.ReadLine();