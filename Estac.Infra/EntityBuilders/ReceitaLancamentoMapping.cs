using Estac.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estac.Infra.EntityBuilders
{
    public class ReceitaLancamentoMapping : IEntityTypeConfiguration<ReceitaLancamento>
    {
        public void Configure(EntityTypeBuilder<ReceitaLancamento> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .UseIdentityColumn(1, 1);

            builder.Property(d => d.Descricao)
               .HasMaxLength(255);

            builder.HasOne(d => d.Receita)
              .WithMany(o => o.ReceitaLancamentos) // Uma receita pertence a um orcamento, um orcamento pode ter várias despesas
              .HasForeignKey(d => d.ReceitaId);
        }
    }
}
