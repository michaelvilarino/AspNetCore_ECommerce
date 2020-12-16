using System.Threading;
using System.Threading.Tasks;
using EcMic.Cliente.API.Models;
using EcMic.Core.Messages;
using EcMic.Core.Utils;
using FluentValidation.Results;
using MediatR;

namespace EcMic.Cliente.API.Application.Commands
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

            var clientExistente = _clienteRepository.ObterPorCpf(cliente.Cpf.Numero);

            if (clientExistente != null)
            {
                AdicionarErro("Cliente já existe!");
                return validationResult;
            }
            
            _clienteRepository.Adicionar(cliente);

            return await PersistirDados(_clienteRepository.UnitOfWork);
        }
    }
}