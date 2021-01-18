using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace EcMic.Identidade.API.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(name: "v1", new OpenApiInfo()
                {
                    Title = "Michael E-Commerce",
                    Description = "Esta API traz a documentação de como usar os serviços de Indetidade",
                    Contact = new OpenApiContact() { Name = "Michael", Email = "michael.vilarino@gmail.com" },
                    License = new OpenApiLicense() { Name = "E-Mic", Url = new Uri("https://www.google.com.br") }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "v1");
            });

            return app;
        }
    }
}
