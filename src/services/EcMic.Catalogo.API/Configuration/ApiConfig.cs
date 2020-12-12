using EcMic.Catalogo.API.Data;
using EMic.WebApi.Core.Identidade;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EcMic.Catalogo.API.Configuration
{
    public static class ApiConfig
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogoContext>(optionsAction: options =>
              options.UseSqlServer(configuration.GetConnectionString(name: "DefaultConnection")));

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("Total", builder =>
                                      builder
                                      .AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()
                                 );
            });
        }

        public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Total");

            app.UseAuthConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
