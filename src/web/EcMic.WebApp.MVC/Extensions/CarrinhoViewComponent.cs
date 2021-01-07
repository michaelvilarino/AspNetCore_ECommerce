using EcMic.WebApp.MVC.Models;
using EcMic.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcMic.WebApp.MVC.Extensions
{
    public class CarrinhoViewComponent: ViewComponent
    {
        private readonly IComprasBffService _comprasBffService;

        public CarrinhoViewComponent(IComprasBffService IComprasBffService)
        {
            _comprasBffService = IComprasBffService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _comprasBffService.ObterQuantidadeCarrinho());
        }
    }
}

