using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace EcMic.Identidade.API.Controllers
{
    [ApiController]
    public abstract class MainController: Controller
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
