using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcMic.Clientes.API.Application.Commands;
using EcMic.Core.Mediator;
using EMic.WebApi.Core.Controllers;
using EMic.WebApi.Core.Usuario;
using EcMic.Clientes.API.Models;

namespace EcMic.Clientes.API.Controllers
{
    public class ClienteController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IAspNetUser _user;
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IMediatorHandler mediatorHandler,
                                 IAspNetUser IAspNetUser,
                                 IClienteRepository IClienteRepository
                                )
        {
            _mediatorHandler = mediatorHandler;
            _user = IAspNetUser;
            _clienteRepository = IClienteRepository;
        }
        
        [HttpGet("clientes")]
        public async Task<IActionResult> Index()
        {
           var resultado = await _mediatorHandler.EnviarComando(
                new RegistrarClienteCommand(
                              Guid.NewGuid(), "Michael",              
                              "michael.melo@teste.com",      
                              "938.016.190-52"));
            
            return CustomResponse(resultado);
        }

        [HttpGet("cliente/endereco")]
        public async Task<IActionResult> ObterEndereco()
        {
            var endereco = await _clienteRepository.ObterEnderecoPorId(_user.ObterUserId());

            return endereco == null ? NotFound() : CustomResponse(endereco);
        }

        [HttpPost("cliente/endereco")]
        public async Task<IActionResult> AdicionarEndereco(AdicionarEnderecoCommand endereco)
        {
            endereco.ClienteId = _user.ObterUserId();
            return CustomResponse(await _mediatorHandler.EnviarComando(endereco));
        }
    }
}
