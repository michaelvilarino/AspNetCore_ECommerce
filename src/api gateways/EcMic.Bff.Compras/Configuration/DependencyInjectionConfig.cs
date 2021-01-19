using EcMic.Bff.Compras.Extensions;
using EcMic.Bff.Compras.Services;
using EcMic.Core.SSL;
using EMic.WebApi.Core.Extensions;
using EMic.WebApi.Core.Usuario;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;

namespace EcMic.Bff.Compras.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IPedidoService, PedidoService>()
                    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                    .AddPolicyHandler(PollyExtensions.EsperarTentar())
                    .AddTransientHttpErrorPolicy(
                     p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)))
                    .ConfigurePrimaryHttpMessageHandler(() => ByPassHttpsSSLCertificate.DesabilitarVerficacaoSSL());

            services.AddHttpClient<ICatalogoService, CatalogoService>()
                    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                    .AddPolicyHandler(PollyExtensions.EsperarTentar())
                    .AddTransientHttpErrorPolicy(
                     p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)))
                    .ConfigurePrimaryHttpMessageHandler(() => ByPassHttpsSSLCertificate.DesabilitarVerficacaoSSL());

            services.AddHttpClient<ICarrinhoService, CarrinhoService>()
                    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                    .AddPolicyHandler(PollyExtensions.EsperarTentar())
                    .AddTransientHttpErrorPolicy(
                     p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)))
                    .ConfigurePrimaryHttpMessageHandler(() => ByPassHttpsSSLCertificate.DesabilitarVerficacaoSSL());

            services.AddHttpClient<IClienteService, ClienteService>()
                    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                    .AddPolicyHandler(PollyExtensions.EsperarTentar())
                    .AddTransientHttpErrorPolicy(
                     p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)))
                    .ConfigurePrimaryHttpMessageHandler(() => ByPassHttpsSSLCertificate.DesabilitarVerficacaoSSL());
        }
    }
}
