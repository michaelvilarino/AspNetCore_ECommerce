using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using EcMic.Pagamento.API.Data;
using EcMic.Pagamento.API.Models;
using EcMic.Pagamento.Facade;
using EMic.WebApi.Core.Usuario;
using EcMic.Pagamento.API.Services;
using EcMic.Pagamento.CardAntiCorruption;
using EcMic.Pagamento.API.Data.Repository;

namespace EcMic.Pagamento.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddScoped<IPagamentoService, PagamentoService>();
            services.AddScoped<IPagamentoFacade, PagamentoCartaoCreditoFacade>();

            services.AddScoped<IPagamentoRepository, PagamentoRepository>();
            services.AddScoped<PagamentosContext>();
        }
    }
}