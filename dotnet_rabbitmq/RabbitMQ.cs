using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace dotnet_rabbitmq;

public class MessageBroker : IDisposable
{
    readonly IConnection connection;
    readonly IModel channel;
    readonly Dictionary<Action<string, Action>, string> consumerTags = [];

    public MessageBroker()
    {
        var factory = new ConnectionFactory() { HostName = "rabbitmq" };
        connection = factory.CreateConnection();
        channel = connection.CreateModel();

        channel.QueueDeclare(queue: "hello",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);
        
        // channel.BasicQos(prefetchSize: 0, prefetchCount: 3, global: false);
    }

    public void Publish(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "",
                            routingKey: "hello",
                            basicProperties: null,
                            body: body);

        Console.WriteLine($"[x] Sent {message}");
    }

    public event Action<string, Action> MessageReceived
    {
        add
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                value(message, ()=>channel.BasicAck(ea.DeliveryTag, false));
            };

            var consumerTag = channel.BasicConsume(queue: "hello",
                                                    autoAck: false,
                                                    consumer: consumer);
            consumerTags.Add(value, consumerTag);
        }
        remove
        {
            if (consumerTags.TryGetValue(value, out var consumerTag))
            {
                channel.BasicCancel(consumerTag);
                consumerTags.Remove(value);
            }
        }
    }

    public void Dispose()
    {
        channel.Dispose();
        connection.Dispose();
    }
}