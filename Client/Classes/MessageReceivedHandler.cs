using System;
using System.Collections.Generic;
using System.Text;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet;

namespace Client.Classes
{
    class MessageReceivedHandler
    {
        public static void Handler(MqttApplicationMessage message)
        {
            Console.WriteLine(message.Topic);
        }


    }
}

