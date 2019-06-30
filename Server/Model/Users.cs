using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Model
{
    public class User
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string publicKey { get; set; }
        public string IV { get; set; }
        public string Ip { get; set; }
        public bool Privacy { get; set; }
        public int LastModified { get; set; }
    }
}
