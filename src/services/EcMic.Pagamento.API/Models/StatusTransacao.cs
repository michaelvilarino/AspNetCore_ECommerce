namespace EcMic.Pagamento.API.Models
{
    public enum StatusTransacao
    {
        Autorizado = 1,
        Pago,
        Negado,
        Estornado,
        Cancelado
    }
}