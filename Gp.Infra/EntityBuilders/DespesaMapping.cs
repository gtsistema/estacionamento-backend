using Gp.Domain.Extensions;
using Gp.Domain.Models;
using Gp.Domain.Models.Enuns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gp.Infra.EntityBuilders
{
    public class DespesaMapping : IEntityTypeConfiguration<Despesa>
    {
        public void Configure(EntityTypeBuilder<Despesa> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .UseIdentityColumn(1, 1);

            builder.HasOne(d => d.Orcamento)
               .WithMany(o => o.Despesas) // Uma despesa pertence a um orcamento, um orcamento pode ter várias despesas
               .HasForeignKey(d => d.OrcamentoId);

            builder.HasMany(d => d.DespesaLancamentos) // Uma Despesa pode ter vários DespesaLancamentos
              .WithOne(dl => dl.Despesa) // Um DespesaLancamento pertence a uma única Despesa
              .HasForeignKey(dl => dl.DespesaId);


            // ===================SEED====
            builder.HasData(new List<Despesa>
            {
                new Despesa
                {
                    Id = 1,
                    OrcamentoId = 4,
                    TipoDespesa = TipoDespesa.Parto,
                    Descricao = TipoDespesa.Parto.GetDescription(),
                    DataCriacao = DateTime.Now,
                    ValorPrevisto = 5000.00m,

                },

                new Despesa
                {
                    Id = 2,
                    OrcamentoId = 2,
                    TipoDespesa = TipoDespesa.Aluguel,
                    Descricao = TipoDespesa.Aluguel.GetDescription(),
                    DataCriacao = DateTime.Now,
                    ValorPrevisto = 1050.00m,
                     MesReferente = MesDoAno.Maio

                },

                 new Despesa
                {
                    Id = 3,
                    OrcamentoId = 2,
                    TipoDespesa = TipoDespesa.Combustivel,
                    Descricao = TipoDespesa.Combustivel.GetDescription(),
                    DataCriacao = DateTime.Now,
                    ValorPrevisto = 240.00m,
                    MesReferente = MesDoAno.Maio

                },

                new Despesa
                {
                    Id = 4,
                    OrcamentoId = 2,
                    TipoDespesa = TipoDespesa.Remedios,
                    Descricao = TipoDespesa.Remedios.GetDescription(),
                    DataCriacao = DateTime.Now,
                    ValorPrevisto = 250.00m,
                    MesReferente = MesDoAno.Maio

                },

                new Despesa
                {
                    Id = 5,
                    OrcamentoId = 2,
                    TipoDespesa = TipoDespesa.Luz,
                    Descricao = TipoDespesa.Luz.GetDescription(),
                    DataCriacao = DateTime.Now,
                    ValorPrevisto = 260.00m,
                    MesReferente = MesDoAno.Maio

                },

                new Despesa
                {
                    Id = 6,
                    OrcamentoId = 2,
                    TipoDespesa = TipoDespesa.CartaoCredito,
                    Descricao = TipoDespesa.CartaoCredito.GetDescription(),
                    DataCriacao = DateTime.Now,
                    ValorPrevisto = 4000.00m,
                    MesReferente = MesDoAno.Maio

                },

                new Despesa
                {
                    Id = 7,
                    OrcamentoId = 2,
                    TipoDespesa = TipoDespesa.Alimentacao,
                    Descricao = TipoDespesa.Alimentacao.GetDescription(),
                    DataCriacao = DateTime.Now,
                    ValorPrevisto = 1400.00m,
                     MesReferente = MesDoAno.Maio

                },

                new Despesa
                {
                    Id = 8,
                    OrcamentoId = 2,
                    TipoDespesa = TipoDespesa.Consorcio,
                    Descricao = TipoDespesa.Consorcio.GetDescription(),
                    DataCriacao = DateTime.Now,
                    ValorPrevisto = 480.00m,
                    MesReferente = MesDoAno.Maio
                },
            });
        }
    }
}
