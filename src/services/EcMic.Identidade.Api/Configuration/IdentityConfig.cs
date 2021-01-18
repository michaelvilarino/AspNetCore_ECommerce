using EcMic.Identidade.API.Data;
using EcMic.Identidade.API.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Security.JwtSigningCredentials;

namespace EcMic.Identidade.API.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration Configuration)
        {
            var appSettingsSection = Configuration.GetSection("ApptokenSettings");
            services.Configure<AppTokenSettings>(appSettingsSection);

            //Gera a chave privada para o JWKS
            services.AddJwksManager(options => options.Algorithm = Algorithm.ES256)
                    .PersistKeysToDatabaseStore<ApplicationDbContext>();//Necessário adicionar a interface ISecurityKeyContext no DbContext

            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString(name: "DefaultConnection"))
            );

            services.AddDefaultIdentity<IdentityUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            //services.AddJwtConfiguration(Configuration);

            return services;
        }
    }
}
