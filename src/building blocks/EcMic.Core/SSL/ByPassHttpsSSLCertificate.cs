using System.Net.Http;

namespace EcMic.Core.SSL
{
    /// <summary>
    /// Desabilita verificação de certificados SSL inválidos
    /// </summary>
    public class ByPassHttpsSSLCertificate
    {
        public static HttpClientHandler DesabilitarVerficacaoSSL()
        {
            var retorno = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                }
            };

            return retorno;
        }
    }
}
