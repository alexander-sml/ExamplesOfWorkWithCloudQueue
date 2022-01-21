using Azure.Messaging.ServiceBus;

// connection string to your Service Bus namespace
var connectionString = "{ConnectionString}";
// name of your Service Bus queue
var queueName = "test";
// number of messages to be sent to the queue
const int numOfMessages = 4;
// the client that owns the connection and can be used to create senders and receivers
await using var client = new ServiceBusClient(connectionString);
// the sender used to publish messages to the queue
await using var sender = client.CreateSender(queueName);
// create a batch 
using var messageBatch = await sender.CreateMessageBatchAsync();

for (var j = 10; j > 0; j--)
{
    for (var i = 1; i <= numOfMessages; i++)
    {
        var message = new ServiceBusMessage($"Message {i}");
        // try adding a message to the batch
        if (!messageBatch.TryAddMessage(message))
        {
            // if it is too large for the batch
            throw new Exception($"The message {i} is too large to fit in the batch.");
        }
    }

    try
    {
        // Use the producer client to send the batch of messages to the Service Bus queue
        await sender.SendMessagesAsync(messageBatch);
        Console.WriteLine($"A batch of {numOfMessages} messages has been published to the queue.");
    }
    catch(Exception ex)
    {
        Console.WriteLine($"Messages has not been published to the queue. Error: {ex.Message}");
    }

    await Task.Delay(1000);
}

Console.WriteLine("Press any key to end the application");
Console.ReadKey();