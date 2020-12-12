using EcMic.Catalogo.API.Data;
using EcMic.Catalogo.API.Data.Repository;
using EcMic.Catalogo.API.Models;
using Microsoft.Extensions.DependencyInjection;

namespace EcMic.Catalogo.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<CatalogoContext>();
        }
    }
}
