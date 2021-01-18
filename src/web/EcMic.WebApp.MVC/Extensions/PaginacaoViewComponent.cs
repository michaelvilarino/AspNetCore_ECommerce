using EcMic.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcMic.WebApp.MVC.Extensions
{
    public class PaginacaoViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke(IPagedList modelPaginado)
        {
            return View(modelPaginado);
        }
    }
}
