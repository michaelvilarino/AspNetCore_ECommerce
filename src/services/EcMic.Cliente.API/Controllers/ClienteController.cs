using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcMic.Clientes.API.Application.Commands;
using EcMic.Core.Mediator;
using EMic.WebApi.Core.Controllers;

namespace EcMic.Clientes.API.Controllers
{
    public class ClienteController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ClienteController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
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
    }
}
