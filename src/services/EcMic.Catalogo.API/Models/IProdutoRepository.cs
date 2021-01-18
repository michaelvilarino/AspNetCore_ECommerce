using EcMic.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcMic.Catalogo.API.Models
{
    public interface IProdutoRepository: IRepository<Produto>
    {
        Task<PagedResult<Produto>> ObterTodos(int pageSize, int pageIndex, string query = null);
        Task<Produto> ObterPorId(Guid id);
        void Adicionar(Produto produto);
        void Atualizar(Produto produto);
        Task<List<Produto>> ObterProdutosPorId(string ids);
    }
}
