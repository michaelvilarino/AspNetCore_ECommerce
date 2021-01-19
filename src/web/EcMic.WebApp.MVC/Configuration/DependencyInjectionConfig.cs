using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using EcMic.WebApp.MVC.Extensions;
using EcMic.WebApp.MVC.Services;
using EcMic.WebApp.MVC.Services.Handlers;
using Microsoft.Extensions.Configuration;
using System;
using Polly;
using Polly.Extensions.Http;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Polly.Retry;
using EMic.WebApi.Core.Usuario;
using EcMic.Core.SSL;

namespace EcMic.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IValidationAttributeAdapterProvider, CpfValidationAttributeAdapterProvider>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            #region HttpServices
            //Serviço para interceptar chamadas httpclient
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>()
                    .AddPolicyHandler(PolyExtensions.EsperarTentar())
                    .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(handledEventsAllowedBeforeBreaking: 5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<ICatalogoService, CatalogoService>()
                    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()//Indica que vai usar a interceptação antes de qualquer request do serviço
                    .AddPolicyHandler(PolyExtensions.EsperarTentar())
                    .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(handledEventsAllowedBeforeBreaking: 5, TimeSpan.FromSeconds(30)))//Serve para para as tentativas de chamadas por 30 segundos após tentar 5 vezes
                    .ConfigurePrimaryHttpMessageHandler(() => ByPassHttpsSSLCertificate.DesabilitarVerficacaoSSL());

            services.AddHttpClient<IComprasBffService, ComprasBffService>()
                    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                    .AddPolicyHandler(PolyExtensions.EsperarTentar())
                    .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(handledEventsAllowedBeforeBreaking: 5, TimeSpan.FromSeconds(30)))
                     .ConfigurePrimaryHttpMessageHandler(() => ByPassHttpsSSLCertificate.DesabilitarVerficacaoSSL());

            services.AddHttpClient<IClienteService, ClienteService>()
                    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                    .AddPolicyHandler(PolyExtensions.EsperarTentar())
                    .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(handledEventsAllowedBeforeBreaking: 5, TimeSpan.FromSeconds(30)))
                     .ConfigurePrimaryHttpMessageHandler(() => ByPassHttpsSSLCertificate.DesabilitarVerficacaoSSL());

            //services.AddHttpClient<ICatalogoService, CatalogoService>()
            //        .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()//Indica que vai usar a interceptação
            //        .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(retryCount: 3, sleepDurationProvider: _ => TimeSpan.FromMilliseconds(600)));

            //services.AddHttpClient("Refit",
            //                        options => { options.BaseAddress = new Uri(configuration.GetSection("CatalogoUrl").Value); })
            //       .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()//Indica que vai usar a interceptação
            //       .AddTypedClient(Refit.RestService.For<ICatalogoServiceRefit>);
            #endregion HttpServices

        }

        /// <summary>
        /// Classe para tratar tentativas de chamadas em serviços
        /// </summary>
        public class PolyExtensions
        {
            public static AsyncRetryPolicy<HttpResponseMessage> EsperarTentar()
            {
                //Policy para tentativas de chamadas do serviço
                var retry = HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .WaitAndRetryAsync(sleepDurations: new[]
                    {
                       TimeSpan.FromSeconds(1),
                       TimeSpan.FromSeconds(5),
                       TimeSpan.FromSeconds(10)
                    }, onRetry: (outcome, timespan, retryCount, context) =>
                    {
                        //TODO grava log das tentativas
                    });

                return retry;
            }
        }

        
    }
}