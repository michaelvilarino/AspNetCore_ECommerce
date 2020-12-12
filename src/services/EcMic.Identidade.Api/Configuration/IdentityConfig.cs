using EcMic.Identidade.API.Data;
using EMic.WebApi.Core.Identidade;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EcMic.Identidade.API.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString(name: "DefaultConnection"))
            );

            services.AddDefaultIdentity<IdentityUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddJwtConfiguration(Configuration);

            return services;
        }
    }
}
