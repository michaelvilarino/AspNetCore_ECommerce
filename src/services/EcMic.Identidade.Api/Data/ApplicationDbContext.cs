using EcMic.Identidade.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Security.JwtSigningCredentials;
using NetDevPack.Security.JwtSigningCredentials.Store.EntityFrameworkCore;

namespace EcMic.Identidade.API.Data
{
    public class ApplicationDbContext : IdentityDbContext, ISecurityKeyContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<SecurityKeyWithPrivate> SecurityKeys { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
