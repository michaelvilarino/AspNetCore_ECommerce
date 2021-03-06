﻿using EcMic.Core.DomainObjects.Data;
using EcMic.Core.Messages;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EcMic.Carrinho.API.Data
{
    public class CarrinhoContext: DbContext, IUnitOfWork
    {
        public CarrinhoContext(DbContextOptions<CarrinhoContext> options) : base(options) {

            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Models.CarrinhoCliente> CarrinhoCliente { get; set; }
        public DbSet<Models.CarrinhoItem> CarrinhoItens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            modelBuilder.Entity<Models.CarrinhoCliente>()
                        .HasIndex(c => c.ClienteId)
                        .HasName("IDX_Cliente");

            modelBuilder.Entity<Models.CarrinhoCliente>()
                        .Ignore(c => c.Voucher)
                        .OwnsOne(c => c.Voucher, v =>
                        {
                            v.Property(vc => vc.Codigo)
                            .HasColumnName("VoucherCodigo")
                            .HasColumnType("varchar(50)");

                            v.Property(vc => vc.TipoDesconto)
                             .HasColumnName("TipoDesconto");

                            v.Property(vc => vc.Percentual)
                             .HasColumnName("Percentual");

                            v.Property(vc => vc.ValorDesconto)
                             .HasColumnName("ValorDesconto");
                        });

            modelBuilder.Entity<Models.CarrinhoCliente>()
                        .HasMany(c => c.Itens)
                        .WithOne(i => i.CarrinhoCliente)
                        .HasForeignKey(c => c.CarrinhoId);

            //Se excluir uma entidade mãe, não exclui os filhos em cascata.
            //foreach (var relationShip in modelBuilder.Model.GetEntityTypes()
            //                                              .SelectMany(e => e.GetForeignKeys()))
            //    relationShip.DeleteBehavior = DeleteBehavior.ClientSetNull;

            foreach (var relationShip in modelBuilder.Model.GetEntityTypes()
                                              .SelectMany(e => e.GetForeignKeys()))
                relationShip.DeleteBehavior = DeleteBehavior.Cascade;
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }

    }
}
