using EcMic.Carrinho.API.Data;
using EcMic.Carrinho.API.Data.Repository;
using EcMic.Carrinho.API.Models;
using Microsoft.Extensions.DependencyInjection;

namespace EcMic.Carrinho.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();
            services.AddScoped<CarrinhoContext>();
        }
    }
}
