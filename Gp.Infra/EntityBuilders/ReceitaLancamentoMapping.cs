using Gp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gp.Infra.EntityBuilders
{
    public class ReceitaLancamentoMapping : IEntityTypeConfiguration<ReceitaLancamento>
    {
        public void Configure(EntityTypeBuilder<ReceitaLancamento> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(d => d.Descricao)
               .HasMaxLength(255);

            builder.HasOne(d => d.Receita)
              .WithMany(o => o.ReceitaLancamentos) // Uma despesa pertence a um orcamento, um orcamento pode ter várias despesas
              .HasForeignKey(d => d.ReceitaId);
        }
    }
}
