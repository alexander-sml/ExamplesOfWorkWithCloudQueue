using System.Net;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace Common;

public static class AmazonSqsClientExtensions
{
    public static async Task<string> GetQueueUrl(this AmazonSQSClient sqsClient, string queueName)
    {
        if (string.IsNullOrEmpty(queueName))
        {
            throw new ArgumentException("Queue name should not be blank.");
        }

        var response = await sqsClient.GetQueueUrlAsync(queueName);

        return response.QueueUrl;
    }
    
    public static async Task<List<Message>> GetMessagesAsync(this AmazonSQSClient sqsClient,  string queueName)
    {
        var queueUrl = await sqsClient.GetQueueUrl(queueName);

        var acc = new List<Message>();

        ReceiveMessageResponse response;
    
        do
        {
            response = await sqsClient.ReceiveMessageAsync(
                new ReceiveMessageRequest
                {
                    QueueUrl = queueUrl,
                    AttributeNames = new List<string> { "ApproximateReceiveCount" },
                    MessageAttributeNames = new List<string> { "All" }
                });
        
            if (response.HttpStatusCode != HttpStatusCode.OK)
            {
                break;
            }

            acc.AddRange(response.Messages);
        } while (response.HttpStatusCode == HttpStatusCode.OK && response.Messages.Count > 0);
    
        return acc;
    }
    
    public static async Task PostMessageAsync(this AmazonSQSClient sqsClient, string queueName, string messageBody)
    {
        var queueUrl = await sqsClient.GetQueueUrl(queueName);

        var sendMessageRequest = new SendMessageRequest
        {
            QueueUrl = queueUrl,
            MessageBody = messageBody,
        };

        await sqsClient.SendMessageAsync(sendMessageRequest);
    }
}