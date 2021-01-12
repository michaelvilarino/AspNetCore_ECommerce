using System.Threading.Tasks;


namespace EcMic.Pagamento.Facade
{
    public interface IPagamentoFacade
    {
        Task<EcMic.Pagamento.API.Models.Transacao> AutorizarPagamento(EcMic.Pagamento.API.Models.Pagamento pagamento);
        Task<EcMic.Pagamento.API.Models.Transacao> CapturarPagamento(EcMic.Pagamento.API.Models.Transacao transacao);
        Task<EcMic.Pagamento.API.Models.Transacao> CancelarAutorizacao(EcMic.Pagamento.API.Models.Transacao transacao);
    }
}