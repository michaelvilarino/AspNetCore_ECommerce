using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcMic.WebApp.MVC.Configuration
{
    public static class IdentityConfig
    {
        public static void AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/login";//Se não tiver logado, é redirecionado para esta rota
                        options.AccessDeniedPath = "/acesso-negado";//Se não tiver permissão, é redirecionado para esta rota
                    });
        }

        public static void UseIdentityConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
