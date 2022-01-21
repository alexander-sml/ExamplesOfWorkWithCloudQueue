using Amazon.SQS;

namespace Common;

public class AmazonSqsClientFactory
{
    const string SERVICE_URL = "https://sqs.mcs.mail.ru";
    
    public static AmazonSQSClient CreateClient(AppConfig appConfig)
    {
        var sqsConfig = new AmazonSQSConfig
        {
            ServiceURL = SERVICE_URL,
        };
        var awsCredentials = new AwsCredentials(appConfig);
        return new AmazonSQSClient(awsCredentials, sqsConfig);
    }
}