using System;
using System.Threading.Tasks;
using EcMic.Core.Messages.Integration;

namespace EcMic.Pagamento.API.Services
{
    public interface IPagamentoService
    {
        Task<ResponseMessage> AutorizarPagamento(EcMic.Pagamento.API.Models.Pagamento pagamento);
        Task<ResponseMessage> CapturarPagamento(Guid pedidoId);
        Task<ResponseMessage> CancelarPagamento(Guid pedidoId);
    }
}