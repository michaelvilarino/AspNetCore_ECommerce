using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EcMic.Core.Utils;
using EcMic.MessageBus;
using EcMic.Pagamento.API.Services;

namespace EcMic.Pagamento.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<PagamentoIntegrationHandler>();
        }
    }
}