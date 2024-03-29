﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MQTTnet.AspNetCore;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        { 
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureKestrel(o =>
            {
                o.ListenAnyIP(1883, l => l.UseMqtt());
                o.ListenAnyIP(5000);
                o.ListenAnyIP(5001, l => l.UseHttps());
            })
                .UseStartup<Startup>();
    }
}
