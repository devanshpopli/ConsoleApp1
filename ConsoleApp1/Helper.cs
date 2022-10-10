using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Helper
    {
        static string topicName = "demo";

        public static async Task CreateTopicAsync(string connectionString)
        {
            // Create the topic if it does not exist already
            ServiceBusAdministrationClient client = new ServiceBusAdministrationClient(connectionString);

            if (!await client.TopicExistsAsync(topicName))
            {
                await client.CreateTopicAsync(topicName);
            }
        }

        public static async Task SendMessageAsync(string connectionString)
        {
            await using var client = new ServiceBusClient(connectionString);
            ServiceBusSender sender = client.CreateSender(topicName);

            // Sends random messages every 10 seconds to the topic
            string[] messages =
            {
        "Employee Id '{0}' has joined.",
        "Employee Id '{0}' has left.",
        "Employee Id '{0}' has switched to a different team."
    };

            while (true)
            {
                Random rnd = new Random();
                string employeeId = rnd.Next(10000, 99999).ToString();
                string notification = String.Format(messages[rnd.Next(0, messages.Length)], employeeId);

                // Send Notification
                ServiceBusMessage message = new ServiceBusMessage(notification);
                long seq = await sender.ScheduleMessageAsync(message,
DateTimeOffset.Now.AddSeconds(30)
);

                //await sender.SendMessageAsync(message);

                Console.WriteLine("{0} Message sent - '{1}'", DateTime.Now, notification);

                System.Threading.Thread.Sleep(new TimeSpan(0, 0, 10));
            }
        }
    }
}
