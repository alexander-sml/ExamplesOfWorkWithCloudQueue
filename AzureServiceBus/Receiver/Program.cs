using Azure.Messaging.ServiceBus;

// connection string to your Service Bus namespace
var connectionString = "{ConnectionString}";
// name of your Service Bus queue
var queueName = "test";
// the client that owns the connection and can be used to create senders and receivers
await using var client = new ServiceBusClient(connectionString);
// the processor that reads and processes messages from the queue
await using var processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

// add handler to process messages
processor.ProcessMessageAsync += async (args) =>
{
    var body = args.Message.Body.ToString();
    Console.WriteLine($"Received: {body}");
    // complete the message. message is deleted from the queue. 
    await args.CompleteMessageAsync(args.Message);
};

// add handler to process any errors
processor.ProcessErrorAsync += (args) =>
{
    Console.WriteLine(args.Exception.ToString());
    return Task.CompletedTask;
};

// start processing 
await processor.StartProcessingAsync();

Console.WriteLine("Wait for a minute and then press any key to end the processing");
Console.ReadKey();

// stop processing 
Console.WriteLine("\nStopping the receiver...");
await processor.StopProcessingAsync();
Console.WriteLine("Stopped receiving messages");