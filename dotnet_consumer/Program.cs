using dotnet_rabbitmq;

using var messageBroker = new MessageBroker();
Console.WriteLine(" [*] Waiting for messages.");
messageBroker.MessageReceived += OnMessageReceived;

ManualResetEvent quitEvent = new(false);

Console.CancelKeyPress += (sender, eArgs) => {
    quitEvent.Set();
    eArgs.Cancel = true;
};

void OnMessageReceived(string message)
{
    Console.WriteLine($"[x] Processing {message}");
    Task.Run(()=>HandleMessageAsync(message));
}

async Task HandleMessageAsync(string message)
{
    await Task.Delay(5000);
    var payload = new Payload(message);
    Console.WriteLine($"[x] Received {payload}");
}

// Instead of waiting for Console.ReadLine, wait on the quitEvent.
quitEvent.WaitOne();