namespace EcMic.Pagamento.MicPag
{
    /// <summary>
    /// Chaves recebidas quando faz cadastro no gateway de pagamento
    /// </summary>
    public class NerdsPagService
    {
        public readonly string ApiKey;
        public readonly string EncryptionKey;// Cada cliente tem sua chave

        public NerdsPagService(string apiKey, string encryptionKey)
        {
            ApiKey = apiKey;
            EncryptionKey = encryptionKey;
        }
    }
}