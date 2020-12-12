namespace EMic.WebApi.Core.Identidade
{
    public class AppSettings
    {
        public string secret { get; set; }
        public int ExpiracaoHoras { get; set; }
        public string Emissor { get; set; }
        public string ValidoEm { get; set; }
    }
}
