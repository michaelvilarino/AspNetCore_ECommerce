using EcMic.Carrinho.API.Models;
using EcMic.Core.DomainObjects.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcMic.Carrinho.API.Data.Repository
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        private readonly CarrinhoContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public CarrinhoRepository(CarrinhoContext context)
        {
            _context = context;
        }

        public void Adicionar(Models.Carrinho carrinho)
        {
            _context.Carrinho.Add(carrinho);
        }

        public void Atualizar(Models.Carrinho carrinho)
        {
            _context.Carrinho.Update(carrinho);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<Models.Carrinho> ObterPorId(Guid id)
        {
            return await _context.Carrinho.FindAsync(id);
        }

        public async Task<IEnumerable<Models.Carrinho>> ObterTodos()
        {
            return await _context.Carrinho.AsNoTracking().ToListAsync();
        }
    }
}
