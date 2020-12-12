using EcMic.WebApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcMic.WebApp.MVC.Services
{
    public interface ICatalogoService
    {
        Task<IEnumerable<ProdutoViewModel>> ObterTodos();
        Task<ProdutoViewModel> ObterPorId(Guid id);
    }
}
