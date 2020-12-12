using EcMic.WebApp.MVC.Extensions;
using EcMic.WebApp.MVC.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EcMic.WebApp.MVC.Services
{
    public class CatalogoService : Service, ICatalogoService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;

        public CatalogoService(HttpClient httpClient,
                                   IOptions<AppSettings> settings)
        {
            _appSettings = settings.Value;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CatalogoUrl);
        }

        public async Task<ProdutoViewModel> ObterPorId(Guid id)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos/{id}");

            TratarErrosResponse(response);
            return await DeserializarObjetoResponse<ProdutoViewModel>(response);

        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        {
            var response = await _httpClient.GetAsync("/catalogo/produtos/");

            TratarErrosResponse(response);
            return  await DeserializarObjetoResponse<IEnumerable<ProdutoViewModel>>(response);            
        }
    }
}
