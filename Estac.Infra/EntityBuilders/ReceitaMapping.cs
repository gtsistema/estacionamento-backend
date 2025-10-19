using Estac.Domain.Extensions;
using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Estac.Domain.Models.ModelEnum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estac.Infra.EntityBuilders
{
    public class ReceitaMapping : IEntityTypeConfiguration<Receita>
    {
        public void Configure(EntityTypeBuilder<Receita> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .UseIdentityColumn(1, 1);

            builder.HasOne(d => d.Orcamento)
               .WithMany(o => o.Receitas) // Uma Receita pertence a um orcamento, um orcamento pode ter várias receitas
               .HasForeignKey(d => d.OrcamentoId);

            builder.HasMany(d => d.ReceitaLancamentos) // Uma Receita pode ter vários ReceitaLancamentos
              .WithOne(dl => dl.Receita) // Um ReceitaLancamento pertence a uma única Receita
              .HasForeignKey(dl => dl.ReceitaId);

            // ===================SEED====
            builder.HasData(new List<Receita>
            {
                new Receita
                {
                    Id = 1,
                    OrcamentoId = 2,
                    TipoReceita = TipoReceita.SalarioJean,
                    Descricao = TipoReceita.SalarioJean.GetDescription(),
                    DataCriacao = DateTime.Now,
                },

                new Receita
                {
                    Id = 2,
                    OrcamentoId = 2,
                    TipoReceita = TipoReceita.FlexBenner,
                    Descricao = TipoReceita.FlexBenner.GetDescription(),
                    DataCriacao = DateTime.Now,
                },

                 new Receita
                {
                    Id = 3,
                    OrcamentoId = 2,
                    TipoReceita = TipoReceita.Bpc,
                    Descricao = TipoReceita.Bpc.GetDescription(),
                    DataCriacao = DateTime.Now,
                },
            });
        }
    }
}
