using Estac.Domain.Extensions;
using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estac.Infra.EntityBuilders
{
    public class DespesaMapping : IEntityTypeConfiguration<Despesa>
    {
        public void Configure(EntityTypeBuilder<Despesa> builder)
        {
            //builder.HasKey(x => x.Id);

            //builder.Property(x => x.Id)
            //    .HasColumnName("Id")
            //    .UseIdentityColumn(1, 1);

            //builder.HasOne(d => d.Orcamento)
            //   .WithMany(o => o.Despesas) // Uma despesa pertence a um orcamento, um orcamento pode ter várias despesas
            //   .HasForeignKey(d => d.OrcamentoId);

            //builder.HasMany(d => d.DespesaLancamentos) // Uma Despesa pode ter vários DespesaLancamentos
            //  .WithOne(dl => dl.Despesa) // Um DespesaLancamento pertence a uma única Despesa
            //  .HasForeignKey(dl => dl.DespesaId);


            // ===================SEED====
        }
    }
}
