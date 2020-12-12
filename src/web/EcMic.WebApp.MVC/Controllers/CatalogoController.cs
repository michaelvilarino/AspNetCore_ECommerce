using EcMic.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;

namespace EcMic.WebApp.MVC.Controllers
{
    public class CatalogoController : MainController
    {
        private readonly ICatalogoService _catalogoService;

        public CatalogoController(ICatalogoService ICatalogoService)
        {
            _catalogoService = ICatalogoService;
        }

        [HttpGet]
        [Route("")]
        [Route("vitrine")]
        public async Task<IActionResult> Index()
        {
            var produtos = await _catalogoService.ObterTodos();

            return View(produtos);
        }

        [HttpGet]
        [Route("Produto-detalhe/{id}")]
        public async Task<IActionResult> ProdutoDetalhe(Guid id)
        {
            var produto = await _catalogoService.ObterPorId(id);

            return View(produto);
        }
            
    }
}
