using Confluent.Kafka;
using System;
using System.Threading;

public class Program
{
    static void Main(string[] args)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9492", // Change this to your Kafka broker's address
            GroupId = "KafkaConsumerGroup",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false // Disable auto commit to manually control message commits
        };

        int numConsumers = 3;
        string topic = "my-topic"; // Change this to your Kafka topic

        var consumers = new IConsumer<string, string>[numConsumers];
        var cts = new CancellationTokenSource();

        Console.WriteLine($"Starting {numConsumers} Kafka consumers in the same group. Press Ctrl+C to exit.");

        try
        {
            for (int i = 0; i < numConsumers; i++)
            {
                consumers[i] = new ConsumerBuilder<string, string>(config).Build();
                consumers[i].Subscribe(topic);
            }

            foreach (var consumer in consumers)
            {
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    while (true)
                    {
                        try
                        {
                            var message = consumer.Consume(cts.Token);
                            Console.WriteLine($"Consumer: Consumed message: {message.Message.Value}, Offset: {message.Offset.Value}");

                            // Simulating some processing time
                            Thread.Sleep(2000);

                            // Manually commit the consumed message offset
                            consumer.Commit(message);
                            Console.WriteLine($"Consumer: Message offset committed.");
                        }
                        catch (ConsumeException ex)
                        {
                            Console.WriteLine($"Consumer: Error consuming message: {ex.Error.Reason}");
                        }
                        catch (OperationCanceledException)
                        {
                            // Consumer has been stopped
                            break;
                        }
                    }
                });
            }

            // Wait for Ctrl+C to stop consumers
            Console.CancelKeyPress += (_, e) => {
                e.Cancel = true;
                cts.Cancel();
            };
            WaitHandle.WaitAll(new[] { cts.Token.WaitHandle });
        }
        finally
        {
            foreach (var consumer in consumers)
            {
                consumer.Close();
            }
        }
    }
}
