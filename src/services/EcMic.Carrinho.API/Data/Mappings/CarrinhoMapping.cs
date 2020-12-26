using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcMic.Carrinho.API.Data.Mappings
{
    public class CarrinhoMapping : IEntityTypeConfiguration<Models.Carrinho>
    {
        public void Configure(EntityTypeBuilder<Models.Carrinho> builder)
        {
            builder.HasKey(keyExpression: c => c.Id);

            builder.ToTable("Carrinho");
        }
    }
}
