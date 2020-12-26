using EMic.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcMic.Carrinho.API.Controllers
{
    [Authorize]
    public class CarrinhoController : MainController
    {
        [Route("Carrinho")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
