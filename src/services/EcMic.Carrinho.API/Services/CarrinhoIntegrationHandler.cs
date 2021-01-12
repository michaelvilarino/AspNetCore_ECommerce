using EcMic.Carrinho.API.Data;
using EcMic.Core.Messages.Integration;
using EcMic.MessageBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EcMic.Carrinho.API.Services
{
    public class CarrinhoIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _messageBus;
        private readonly IServiceProvider _serviceProvider;

        public CarrinhoIntegrationHandler(IMessageBus IMessageBus, IServiceProvider IServiceProvider)
        {
            _messageBus      = IMessageBus;
            _serviceProvider = IServiceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetSubscribers();
            return Task.CompletedTask; 
        }

        private void SetSubscribers()
        {
            _messageBus.SubscribeAsync<PedidoRealizadoIntegrationEvent>("PedidoRealizado", async request =>
             await ApagarCarrinho(request));
        }

        private async Task ApagarCarrinho(PedidoRealizadoIntegrationEvent message)
        {
            var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CarrinhoContext>();

            var carrinho = await context.CarrinhoCliente
                                        .FirstOrDefaultAsync(f => f.ClienteId == message.ClienteId);

            if(carrinho != null)
            {
                context.CarrinhoCliente.Remove(carrinho);
                await context.SaveChangesAsync();
            }
        }
    }
}
