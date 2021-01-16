using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EcMic.Core.Utils;
using EcMic.MessageBus;
using EcMic.Pedido.API.Services;

namespace EcMic.Pedido.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                    .AddHostedService<PedidoOrquestradorIntegrationHandler>()
                    .AddHostedService<PedidoIntegrationHandler>();
        }
    }
}