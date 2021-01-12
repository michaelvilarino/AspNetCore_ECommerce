using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EcMic.Core.DomainObjects;

namespace EcMic.Pagamento.API.Models
{
    public interface IPagamentoRepository : IRepository<EcMic.Pagamento.API.Models.Pagamento>
    {
        void AdicionarPagamento(Pagamento pagamento);
        void AdicionarTransacao(Transacao transacao);
        Task<Pagamento> ObterPagamentoPorPedidoId(Guid pedidoId);
        Task<IEnumerable<Transacao>> ObterTransacaoesPorPedidoId(Guid pedidoId);
    }
}