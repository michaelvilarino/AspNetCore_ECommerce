using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EcMic.Core.DomainObjects;

namespace EcMic.Carrinho.API.Models
{
    public interface ICarrinhoRepository: IRepository<Carrinho>
    {
        Task<IEnumerable<Carrinho>> ObterTodos();
        Task<Carrinho> ObterPorId(Guid id);
        void Adicionar(Carrinho carrinho);
        void Atualizar(Carrinho carrinho);
    }
}
