using System;
using System.Collections.Generic;
using System.Text;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Client.Classes
{
    class MqttStorage : IManagedMqttClientStorage
    {
        private string Filename = @"QueuedMessages.json";
        
        public Task SaveQueuedMessagesAsync(IList<ManagedMqttApplicationMessage> messages)
        {
            File.WriteAllText(Filename, JsonConvert.SerializeObject(messages));
            return Task.FromResult(0);
        }

        public Task<IList<ManagedMqttApplicationMessage>> LoadQueuedMessagesAsync()
        {
            IList<ManagedMqttApplicationMessage> retainedMessages;
            if (File.Exists(Filename))
            {
                var json = File.ReadAllText(Filename);
                retainedMessages = JsonConvert.DeserializeObject<List<ManagedMqttApplicationMessage>>(json);
            }
            else
            {
                retainedMessages = new List<ManagedMqttApplicationMessage>();
            }

            return Task.FromResult(retainedMessages);
        }
    }
}
