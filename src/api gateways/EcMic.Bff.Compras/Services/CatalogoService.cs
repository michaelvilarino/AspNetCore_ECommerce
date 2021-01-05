using System;
using System.Net.Http;
using Microsoft.Extensions.Options;
using EcMic.Bff.Compras.Extensions;
using EcMic.Bff.Compras.Models;
using System.Threading.Tasks;

namespace EcMic.Bff.Compras.Services
{
    public interface ICatalogoService
    {
        Task<ItemProdutoDTO> ObterPorId(Guid Id);
    }

    public class CatalogoService : Service, ICatalogoService
    {
        private readonly HttpClient _httpClient;

        public CatalogoService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CatalogoUrl);
        }

        public async Task<ItemProdutoDTO> ObterPorId(Guid Id)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos/{Id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<ItemProdutoDTO>(response);
        }
    }
}