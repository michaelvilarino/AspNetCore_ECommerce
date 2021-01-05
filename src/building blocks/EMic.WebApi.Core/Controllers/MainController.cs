using System.Collections.Generic;
using System.Linq;
using EcMic.Core.Communication;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EMic.WebApi.Core.Controllers
{
    [ApiController]
    public abstract class MainController : Controller
    {
        protected ICollection<string> erros = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", erros.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(s => s.Errors);

            foreach (var erro in erros)
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ResponseResult resposta)
        {
            ResponsePossuiErros(resposta);

            return CustomResponse();
        }

        protected bool ResponsePossuiErros(ResponseResult resposta)
        {
            if (resposta == null && !resposta.Errors.Mensagens.Any()) return false;
            
            foreach (var mensagem in resposta.Errors.Mensagens)
            {
                AdicionarErroProcessamento(mensagem);
            }

            return true;
        }

        protected bool OperacaoValida()
        {
            return !erros.Any();
        }

        protected void AdicionarErroProcessamento(string erro)
        {
            erros.Add(erro);
        }

        protected void LimparErrosProcessamento()
        {
            erros.Clear();
        }
    }
}
