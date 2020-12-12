using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using EcMic.WebApp.MVC.Extensions;
using EcMic.WebApp.MVC.Models;

namespace EcMic.WebApp.MVC.Services
{
    public class AutenticacaoService : Service, IAutenticacaoService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;

        public AutenticacaoService(HttpClient httpClient, 
                                   IOptions<AppSettings> settings)
        {
            _appSettings = settings.Value;
            _httpClient  = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.AutenticacaoUrl);            
        }

        public async Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin)
        {
            var loginContent = ObterConteudo(usuarioLogin);

            var response = await _httpClient.PostAsync("/api/identidade/autenticar", loginContent);

            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLogin
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }

            return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
        }

        public async Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro)
        {
            var registroContent = ObterConteudo(usuarioRegistro);

            var response = await _httpClient.PostAsync("/api/identidade/nova-conta", registroContent);

            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLogin
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }

            return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
        }
    }
}