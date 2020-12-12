using EcMic.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcMic.WebApp.MVC.Controllers
{
    public class MainController: Controller
    {
        protected bool ResponsePossuiErros(ResponseResult resposta)
        {
            if (resposta != null && resposta.Errors.Mensagens.Any())
            {
                foreach (var mensagem in resposta.Errors.Mensagens)
                {
                    ModelState.AddModelError(string.Empty, mensagem);
                }

                return true;
            }

            return false;
        }
    }
}
