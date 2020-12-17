using EcMic.Catalogo.API.Models;
using EcMic.Core.DomainObjects.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using EcMic.Core.Messages;
using FluentValidation.Results;

namespace EcMic.Catalogo.API.Data
{
    public class CatalogoContext: DbContext, IUnitOfWork
    {
        public CatalogoContext(DbContextOptions<CatalogoContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogoContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
