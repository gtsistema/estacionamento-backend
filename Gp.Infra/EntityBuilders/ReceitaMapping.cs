using Gp.Domain.Extensions;
using Gp.Domain.Models;
using Gp.Domain.Models.Enuns;
using Gp.Domain.Models.ModelEnum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gp.Infra.EntityBuilders
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
                    TIpoReceita = TipoReceita.SalarioJean,
                    Descricao = TipoReceita.SalarioJean.GetDescription(),
                    DataCriacao = DateTime.Now,
                    ValorPrevisto = 3240.00m,
                },

                new Receita
                {
                    Id = 2,
                    OrcamentoId = 2,
                    TIpoReceita = TipoReceita.FlexBenner,
                    Descricao = TipoReceita.FlexBenner.GetDescription(),
                    DataCriacao = DateTime.Now,
                    ValorPrevisto = 1250.00m,
                },

                 new Receita
                {
                    Id = 3,
                    OrcamentoId = 2,
                    TIpoReceita = TipoReceita.Bpc,
                    Descricao = TipoReceita.Bpc.GetDescription(),
                    DataCriacao = DateTime.Now,
                    ValorPrevisto = 1450.00m,
                },
            });
        }
    }
}
