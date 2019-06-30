using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Client.Criptography
{
    class KeyManager
    {
        private static string Filename = @"KeyManager.json";

        public static void AddNewKey(SimmetricKeyMQTT Key)
        {
            //TODO read if the key exist and onlyh update it not append
            //if (GetKeyAndIV(Key.Topic) != null)
            File.AppendAllText(Filename, JsonConvert.SerializeObject(Key));
        }

        public static AesKey GetKeyAndIV(string Topic)
        {
            List<SimmetricKeyMQTT> Keys;
            var json = File.ReadAllText(Filename);
            Keys = JsonConvert.DeserializeObject<List<SimmetricKeyMQTT>>(json);
            AesKey Key = Keys.Find(x => x.Topic==Topic).Key;
            return Key;
        }
    }
}
