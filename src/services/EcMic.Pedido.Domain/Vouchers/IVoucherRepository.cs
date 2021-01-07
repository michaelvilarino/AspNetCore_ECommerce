using System.Threading.Tasks;
using EcMic.Core.DomainObjects;

namespace EcMic.Pedido.Domain
{
    public interface IVoucherRepository : IRepository<Voucher>
    {
        Task<Voucher> ObterVoucherPorCodigo(string codigo);
        void Atualizar(Voucher voucher);
    }
}