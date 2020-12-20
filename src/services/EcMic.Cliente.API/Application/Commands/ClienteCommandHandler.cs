using System.Threading;
using System.Threading.Tasks;
using EcMic.Clientes.API.Application.Events;
using EcMic.Clientes.API.Models;
using EcMic.Core.Messages;
using FluentValidation.Results;
using MediatR;

namespace EcMic.Clientes.API.Application.Commands
{
    public class ClienteCommandHandler: CommandHandler, IRequestHandler<RegistrarClienteCommand, ValidationResult>
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
            

        public async Task<ValidationResult> Handle(
                                               RegistrarClienteCommand message, 
                                               CancellationToken cancellationToken  
                                            )
        {
            if (!message.EhValido()) return message.ValidationResult;

            var cliente = new Models.Cliente(message.Id, message.Nome, message.Email, message.Cpf);

            var clientExistente = await _clienteRepository.ObterPorCpf(cliente.Cpf.Numero);

            if (clientExistente != null)
            {
                AdicionarErro("Cliente já existe!");
                return validationResult;
            }
            
            _clienteRepository.Adicionar(cliente);
            
            cliente.AdicionarEvento(new ClienteRegistradoEvent(message.Id, message.Nome, message.Email, message.Cpf));

            return await PersistirDados(_clienteRepository.UnitOfWork);
        }
    }
}