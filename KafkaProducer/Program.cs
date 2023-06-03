using Confluent.Kafka;
using System;

public class Program
{
    static void Main(string[] args)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092", // Change this to your Kafka broker's address
            ClientId = "KafkaProducerExample"
        };

        using (var producer = new ProducerBuilder<string, string>(config).Build())
        {
            string topic = "my-topic"; // Change this to your Kafka topic

            Console.WriteLine("Enter a message to produce (or 'exit' to quit):");

            string message;
            while ((message = Console.ReadLine()) != "exit")
            {
                var deliveryReport = producer.ProduceAsync(topic, new Message<string, string>
                {
                    Key = null,
                    Value = message
                }).Result;

                Console.WriteLine($"Produced message to: {deliveryReport.TopicPartitionOffset}");
            }
        }
    }
}
