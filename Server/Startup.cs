using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MQTTnet.AspNetCore;
using MQTTnet.Protocol;
using MQTTnet.Server;
using Server.Model;
using Server.MQTT;
using System;
//using System.Reflection;
//using System.Security.Authentication;
//using System.Security.Cryptography.X509Certificates;
//using System.IO;

namespace Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = Configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //var currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //var certificate = new X509Certificate2(Path.Combine(currentPath, "certificate.pfx"), "yourPassword", X509KeyStorageFlags.Exportable);

            services.Add(new ServiceDescriptor(typeof(ChatByKeyContext), new ChatByKeyContext(Configuration.GetConnectionString("DefaultConnection"))));
            var mqttServerOptions = new MqttServerOptionsBuilder()
                .WithClientId("server")
                .WithConnectionValidator(c =>
                {
                    if (c.ClientId.Length < 10)
                    {
                        Console.WriteLine("Client ID too short from" + c.ClientId);
                        c.ReasonCode = MqttConnectReasonCode.ClientIdentifierNotValid;
                        return;
                    }

                    if (c.Username != "ChatByKey")
                    {
                        Console.WriteLine("Wrong username from " + c.ClientId + " used " + c.Username);
                        c.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
                        return;
                    }

                    if (c.Password != "DellaSwag123")
                    {
                        Console.WriteLine("Wrong password from" + c.ClientId + " used " + c.Password);
                        c.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
                        return;
                    }

                    Console.WriteLine("Accesso da " + c.ClientId);
                    c.ReasonCode = MqttConnectReasonCode.Success;
                })
                .WithSubscriptionInterceptor(s =>
                {
                    Console.WriteLine("Subscription from " + s.ClientId + "for topic" + s.TopicFilter.Topic + "with QoS level" + s.TopicFilter.QualityOfServiceLevel);
                })
                .WithApplicationMessageInterceptor(m =>
                {
                    Console.WriteLine("Message arrived from " + m.ClientId + " for topic " + m.ApplicationMessage.Topic + " with QoS level " + m.ApplicationMessage.QualityOfServiceLevel + " and retain flag set to " + m.ApplicationMessage.Retain);
                })
                .WithPersistentSessions()
                .WithDefaultEndpoint()
                .WithDefaultEndpointPort(1883)
                .WithStorage(new RetainedMessageHandler())
                //.WithEncryptedEndpoint()
                //.WithEncryptedEndpointPort(config.Port)
                //.WithEncryptionCertificate(certificate.Export(X509ContentType.Pfx))
                //.WithEncryptionSslProtocol(SslProtocols.Tls12)
                .Build();
            services.AddHostedMqttServer(mqttServerOptions);
            services.AddMqttConnectionHandler();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
