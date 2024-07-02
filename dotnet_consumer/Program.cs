using dotnet_rabbitmq;

using var messageBroker = new MessageBroker();
Console.WriteLine(" [*] Waiting for messages.");
messageBroker.MessageReceived += OnMessageReceived;

ManualResetEvent quitEvent = new(false);

Console.CancelKeyPress += (sender, eArgs) => {
    quitEvent.Set();
    eArgs.Cancel = true;
};

void OnMessageReceived(string message, Action ack)
{
    Console.WriteLine($"[x] Processing {message}");
    Task.Run(()=>HandleMessageAsync(message, ack));
}

async Task HandleMessageAsync(string message, Action ack)
{
    var delay = new Random().Next(1000, 5000);
    await Task.Delay(delay);
    var payload = new Payload(message);
    Console.WriteLine($"[x] Received {payload}");
    ack.Invoke();
}

// Instead of waiting for Console.ReadLine, wait on the quitEvent.
quitEvent.WaitOne();