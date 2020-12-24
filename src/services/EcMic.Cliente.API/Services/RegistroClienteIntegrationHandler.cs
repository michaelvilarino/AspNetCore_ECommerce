using EasyNetQ;
using EcMic.Clientes.API.Application.Commands;
using EcMic.Core.Mediator;
using EcMic.Core.Messages.Integration;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EcMic.Clientes.API.Services
{
    public class RegistroClienteIntegrationHandler : BackgroundService
    {
        private IBus _ibus;
        private readonly IServiceProvider _serviceProvider;

        public RegistroClienteIntegrationHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _ibus = RabbitHutch.CreateBus(connectionString: "host=localhost");

            _ibus.Rpc.RespondAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(responder: async request =>
            new ResponseMessage(await RegistrarCliente(request)));

            return Task.CompletedTask;
        }

        private async Task<ValidationResult> RegistrarCliente(UsuarioRegistradoIntegrationEvent message)
        {
            var clienteCommand = new RegistrarClienteCommand(message.Id, message.Nome, message.Email, message.Cpf);
            ValidationResult sucesso;

            using(var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                sucesso = await mediator.EnviarComando(clienteCommand);
            }

            return sucesso;
        }
    }
}
