using Estac.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estac.Infra.EntityBuilders
{
    public class DespesaPagamentoMapping : IEntityTypeConfiguration<DespesaPagamento>
    {
        public void Configure(EntityTypeBuilder<DespesaPagamento> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .UseIdentityColumn(1, 1);

            builder.Property(d => d.Descricao)
               .HasMaxLength(255);

            builder.HasOne(d => d.Despesa)
              .WithMany(o => o.DespesaPagamentos) // Uma despesa pertence a um orcamento, um orcamento pode ter várias despesas
              .HasForeignKey(d => d.DespesaId);
        }
    }
}
