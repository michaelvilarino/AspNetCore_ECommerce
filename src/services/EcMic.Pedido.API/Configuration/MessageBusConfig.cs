using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EcMic.Core.Utils;
using EcMic.MessageBus;

namespace EcMic.Pedidos.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"));
        }
    }
}