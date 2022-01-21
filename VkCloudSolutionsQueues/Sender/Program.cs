using Common;

const string QUEUE_NAME = "user-order";

var appConfig = GetAppConfig();

using var sqsClient = AmazonSqsClientFactory.CreateClient(appConfig);

await sqsClient.PostMessageAsync(QUEUE_NAME, $"Hello! It is my message! [{DateTime.Now}]");

Console.WriteLine("Message sent");
Console.WriteLine("Press any key to end the application");
Console.ReadKey();

static AppConfig GetAppConfig()
    => new AppConfig()
    {
        AwsAccessKey = "{AccessKey}",
        AwsSecretKey = "{AccessSecret}",
        AwsQueueName = "user-order",
    };
    