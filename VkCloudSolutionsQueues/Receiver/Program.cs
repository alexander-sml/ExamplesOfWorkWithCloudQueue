using Common;

const string QUEUE_NAME = "user-order";

var appConfig = GetAppConfig();

using var sqsClient = AmazonSqsClientFactory.CreateClient(appConfig);

var messages = await sqsClient.GetMessagesAsync(QUEUE_NAME);

foreach (var message in messages)
{
    Console.WriteLine(message.Body);
}

Console.WriteLine("All messages handled!");
Console.WriteLine("Press any key to end the application");
Console.ReadKey();

static AppConfig GetAppConfig()
    => new AppConfig()
    {
        AwsAccessKey = "{AccessKey}",
        AwsSecretKey = "{AccessSecret}",
        AwsQueueName = "user-order",
    };
    