using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using EcMic.WebApp.MVC.Extensions;
using EcMic.WebApp.MVC.Services;

namespace EcMic.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();
            services.AddHttpClient<ICatalogoService, CatalogoService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}