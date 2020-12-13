using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using EcMic.WebApp.MVC.Extensions;
using EcMic.WebApp.MVC.Services;
using EcMic.WebApp.MVC.Services.Handlers;

namespace EcMic.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //Serviço para interceptar chamadas httpclient
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();
            services.AddHttpClient<ICatalogoService, CatalogoService>()
                    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();//Indica que vai usar a interceptação

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}