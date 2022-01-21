﻿namespace Common;

public class AppConfig
{
    private const string FifoSuffix = ".fifo";
    private string _queueName;

    public string AwsAccessKey { get; set; }
    public string AwsSecretKey { get; set; }
    public string AwsQueueName
    {
        get => AwsQueueIsFifo ? _queueName + FifoSuffix : _queueName;
        set => _queueName = value;
    }
    public string AwsDeadLetterQueueName
    {
        get
        {
            var deadLetter = _queueName + "-exceptions";
            return AwsQueueIsFifo ? deadLetter + FifoSuffix : deadLetter;
        }
    }

    public bool AwsQueueAutomaticallyCreate { get; set; }
    public bool AwsQueueIsFifo { get; set; }
    public int AwsQueueLongPollTimeSeconds { get; set; }
}