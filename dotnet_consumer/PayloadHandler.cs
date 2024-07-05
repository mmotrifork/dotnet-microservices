using dotnet_consumer.Model;
using dotnet_rabbitmq;

namespace dotnet_consumer
{
    public class PayloadHandler
    {
        private readonly IPersistenceManager persistenceManager;
        private readonly IMessageBroker messageBroker;

        public PayloadHandler(IPersistenceManager persistenceManager, IMessageBroker messageBroker)
        {
            this.persistenceManager = persistenceManager;
            this.messageBroker = messageBroker;
        }

        public void StartReceivingMessages()
        {
            messageBroker.MessageReceived += OnMessageReceived;
        }

        private void OnMessageReceived(string message, Action ack)
        {
            Console.WriteLine($"[ ] Processing {message}");
            Task.Run(() => HandleMessageAsync(message, ack));
        }

        private async Task HandleMessageAsync(string message, Action ack)
        {
            await HandlePayloadAsync(message.ToPayload());
            ack.Invoke();
        }

        public async Task HandlePayloadAsync(dotnet_rabbitmq.Payload payload)
        {
            var cutoff = payload.TimeStamp.AddMinutes(1);
            if (cutoff < DateTime.UtcNow)
            {
                Console.WriteLine($"[-] Discarding {payload}");
                return;
            }
            else
            {
                if (payload.TimeStamp.Second.IsEven()) // Corrected IsEven check
                {
                    Console.WriteLine($"[+] Saving {payload}");
                    await persistenceManager.SavePayloadAsync(payload);
                }
                else
                {
                    payload.Count++;
                    Console.WriteLine($"[ ] Re-Publishing {payload}");
                    messageBroker.Publish(payload.ToJson());
                }
            }
        }
    }
}