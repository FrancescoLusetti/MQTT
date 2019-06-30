using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Criptography
{
    class SimmetricKeyMQTT
    {
        public string Topic;
        public AesKey Key;
    }

    class AesKey
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
    }
}
