using Confluent.Kafka;
using System;
using System.Threading;

public class Program
{
    static void Main(string[] args)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092", // Change this to your Kafka broker's address
            GroupId = "Atul",
             AutoOffsetReset = AutoOffsetReset.Earliest//Remove This If want to Read from Specific OffSet
        };

        using (var consumer = new ConsumerBuilder<string, string>(config).Build())
        {
            string topic = "my-topic"; // Change this to your Kafka topic

         
            //TopicPartitionOffset partitionOffset = new TopicPartitionOffset(new TopicPartition(topic, 0), new Offset(5)); // Change the partition and offset values as per your requirement
            //consumer.Assign(new[] { partitionOffset });

            
            consumer.Subscribe(topic);

            Console.WriteLine("Consuming messages from Kafka. Press Ctrl+C to exit.");

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) => {
                e.Cancel = true;
                cts.Cancel();
            };

            try
            {
                while (true)
                {
                    var message = consumer.Consume(cts.Token);
                    Console.WriteLine($"Consumed message: {message.Message.Value}");
                }
            }
            catch (OperationCanceledException)
            {
                // Consumer has been stopped
            }
            finally
            {
                consumer.Close();
            }
        }
    }
}
