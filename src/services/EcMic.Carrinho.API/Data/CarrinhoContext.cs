using EcMic.Core.DomainObjects.Data;
using EcMic.Core.Messages;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EcMic.Carrinho.API.Data
{
    public class CarrinhoContext: DbContext, IUnitOfWork
    {
        public CarrinhoContext(DbContextOptions<CarrinhoContext> options) : base(options) { }

        public DbSet<Models.Carrinho> Carrinho { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarrinhoContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }

    }
}
