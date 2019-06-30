using System;
using System.Collections.Generic;
using System.Text;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet;
using MQTTnet.Client.Options;
using System.Threading.Tasks;

namespace Client.Classes
{
    class StartClient
    {
        public static async Task<IManagedMqttClient> RunAsync(string Username, string Password, string Server)
        {
            var options = new ManagedMqttClientOptionsBuilder()
                .WithClientOptions(new MqttClientOptionsBuilder()
                    .WithClientId("TestManagedClient")
                    .WithCredentials("ChatByKey", "DellaSwag123")
                    .WithTcpServer("localhost", 1883)
                    .Build())
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(1))
                .WithStorage(new MqttStorage())
                .Build();
            var managedClient = new MqttFactory().CreateManagedMqttClient();
            managedClient.UseApplicationMessageReceivedHandler(h =>
            {
                Console.WriteLine(h.ClientId + ": " + h.ApplicationMessage.Payload);
            });
            await managedClient.StartAsync(options);

            await managedClient.PublishAsync(builder => builder.WithExactlyOnceQoS().WithPayload("swag").WithTopic("xyz"));
            Console.ReadLine();
            return managedClient;
        }
    }
}
