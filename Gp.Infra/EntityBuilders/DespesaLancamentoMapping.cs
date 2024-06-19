using Gp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gp.Infra.EntityBuilders
{
    public class DespesaLancamentoMapping : IEntityTypeConfiguration<DespesaLancamento>
    {
        public void Configure(EntityTypeBuilder<DespesaLancamento> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(d => d.Descricao)
               .HasMaxLength(255);

            builder.HasOne(d => d.Despesa)
              .WithMany(o => o.DespesaLancamentos) // Uma despesa pertence a um orcamento, um orcamento pode ter várias despesas
              .HasForeignKey(d => d.DespesaId);
        }
    }
}
