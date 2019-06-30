using System;
using System.Threading.Tasks;
using Client.Classes;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;

namespace Client
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine($"MQTTnet - TestApp.{TargetFrameworkInfoProvider.TargetFramework}");
            //Console.WriteLine("1 = Start client");
            //Console.WriteLine("2 = Start server");
            //Console.WriteLine("3 = Start performance test");
            //Console.WriteLine("4 = Start managed client");
            //Console.WriteLine("5 = Start public broker test");
            //Console.WriteLine("6 = Start server & client");
            //Console.WriteLine("7 = Client flow test");
            //Console.WriteLine("8 = Start performance test (client only)");
            //Console.WriteLine("9 = Start server (no trace)");
            //Console.WriteLine("a = Start QoS 2 benchmark");
            //Console.WriteLine("b = Start QoS 1 benchmark");
            //Console.WriteLine("c = Start QoS 0 benchmark");

            //var pressedKey = Console.ReadKey(true);
            //if (pressedKey.KeyChar == '1')
            //{
            //    Task.Run(ClientTest.RunAsync);
            //}
            //else if (pressedKey.KeyChar == '2')
            //{
            //    Task.Run(ServerTest.RunAsync);
            //}
            //else if (pressedKey.KeyChar == '3')
            //{
            //    PerformanceTest.RunClientAndServer();
            //    return;
            //}
            //else if (pressedKey.KeyChar == '4')
            //{
            //    Task.Run(ManagedClientTest.RunAsync);
            //}
            //else if (pressedKey.KeyChar == '5')
            //{
            //    Task.Run(PublicBrokerTest.RunAsync);
            //}
            //else if (pressedKey.KeyChar == '6')
            //{
            //    Task.Run(ServerAndClientTest.RunAsync);
            //}
            //else if (pressedKey.KeyChar == '7')
            //{
            //    Task.Run(ClientFlowTest.RunAsync);
            //}
            //else if (pressedKey.KeyChar == '8')
            //{
            //    PerformanceTest.RunClientOnly();
            //    return;
            //}
            //else if (pressedKey.KeyChar == '9')
            //{
            //    ServerTest.RunEmptyServer();
            //    return;
            //}
            //else if (pressedKey.KeyChar == 'a')
            //{
            //    Task.Run(PerformanceTest.RunQoS2Test);
            //}
            //else if (pressedKey.KeyChar == 'b')
            //{
            //    Task.Run(PerformanceTest.RunQoS1Test);
            //}
            //else if (pressedKey.KeyChar == 'c')
            //{
            //    Task.Run(PerformanceTest.RunQoS0Test);
            //}

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
                //TODO: use the code to do the check on cryptography
                Console.WriteLine(h.ClientId + ": " + h.ApplicationMessage.Payload);
            });
            managedClient.StartAsync(options);

            //await managedClient.SubscribeAsync(new TopicFilter { Topic = "xyz", QualityOfServiceLevel = MqttQualityOfServiceLevel.AtMostOnce });

            managedClient.PublishAsync(builder=> builder.WithExactlyOnceQoS().WithPayload("swag").WithTopic("xyz"));
            Console.ReadLine();
            return;
        }
    }
}
