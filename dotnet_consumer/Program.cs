using dotnet_consumer;
using dotnet_consumer.Model;
using dotnet_rabbitmq;
using Microsoft.EntityFrameworkCore;

using var messageBroker = new MessageBroker();
Console.WriteLine("Database migration started...");
// Removed the using statement for payloadContext
using (var payloadContext = new PayloadContext())
{
    await payloadContext.Database.MigrateAsync();
}
var persistenceManager = new PersistenceManager();
var payloadHandler = new PayloadHandler(persistenceManager, messageBroker);

ManualResetEvent quitEvent = new(false);

bool isExiting = false; // Step 1: Add a flag to indicate the application is exiting.

Console.CancelKeyPress += (sender, eArgs) => {
    isExiting = true; // Set the flag to true when the application is about to exit.
    quitEvent.Set();
    eArgs.Cancel = true;
};

var timer = new System.Timers.Timer(30000);
timer.Elapsed += async (sender, e) => {
    if (isExiting) return; // Step 2: Check the flag before accessing the database.

    Console.WriteLine("Fetching and printing database content...");
    using var payloadContext = new PayloadContext();
    var payloads = await payloadContext.GetAllPayloadsAsync();
    foreach (var payload in payloads)
    {
        Console.WriteLine($"{payload.PayloadId} - {payload.TimeStamp} - {payload.Count}");
    }
};
timer.Enabled = true;

Console.WriteLine(" [*] Waiting for messages.");
payloadHandler.StartReceivingMessages();

quitEvent.WaitOne();

timer.Stop(); // Step 3: Stop the timer before disposing of the context.
timer.Dispose();