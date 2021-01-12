using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EcMic.Pagamento.API.Models;
using EcMic.Core.DomainObjects.Data;

namespace EcMic.Pagamento.API.Data.Repository
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly PagamentosContext _context;

        public PagamentoRepository(PagamentosContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void AdicionarPagamento(EcMic.Pagamento.API.Models.Pagamento pagamento)
        {
            _context.Pagamentos.Add(pagamento);
        }

        public void AdicionarTransacao(Transacao transacao)
        {
            _context.Transacoes.Add(transacao);
        }

        public async Task<EcMic.Pagamento.API.Models.Pagamento> ObterPagamentoPorPedidoId(Guid pedidoId)
        {
            return await _context.Pagamentos.AsNoTracking()
                .FirstOrDefaultAsync(p => p.PedidoId == pedidoId);
        }

        public async Task<IEnumerable<Transacao>> ObterTransacaoesPorPedidoId(Guid pedidoId)
        {
            return await _context.Transacoes.AsNoTracking()
                .Where(t => t.Pagamento.PedidoId == pedidoId).ToListAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}