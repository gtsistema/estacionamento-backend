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
                    OrcamentoId = 2,
                    TipoDespesa = TipoDespesa.Gás,
                    Descricao = TipoDespesa.Gás.GetDescription(),
                    DataCriacao = DateTime.Now,
                    ValorPrevisto = 43.50m,
                },

                new Despesa
                {
                    Id = 2,
                    OrcamentoId = 2,
                    TipoDespesa = TipoDespesa.Aluguel,
                    Descricao = TipoDespesa.Aluguel.GetDescription(),
                    DataCriacao = DateTime.Now,
                    ValorPrevisto = 1250.00m,
                },

                 new Despesa
                {
                    Id = 3,
                    OrcamentoId = 2,
                    TipoDespesa = TipoDespesa.Combustivel,
                    Descricao = TipoDespesa.Combustivel.GetDescription(),
                    DataCriacao = DateTime.Now,
                    ValorPrevisto = 500.00m,
                },

                new Despesa
                {
                    Id = 4,
                    OrcamentoId = 2,
                    TipoDespesa = TipoDespesa.Remedios,
                    Descricao = TipoDespesa.Remedios.GetDescription(),
                    DataCriacao = DateTime.Now,
                    ValorPrevisto = 150.00m,
                },

                new Despesa
                {
                    Id = 5,
                    OrcamentoId = 2,
                    TipoDespesa = TipoDespesa.Luz,
                    Descricao = TipoDespesa.Luz.GetDescription(),
                    DataCriacao = DateTime.Now,
                    ValorPrevisto = 260.00m,
                },

                new Despesa
                {
                    Id = 6,
                    OrcamentoId = 2,
                    TipoDespesa = TipoDespesa.CartaoCredito,
                    Descricao = TipoDespesa.CartaoCredito.GetDescription(),
                    DataCriacao = DateTime.Now,
                    ValorPrevisto = 1300.00m,
                },

                new Despesa
                {
                    Id = 7,
                    OrcamentoId = 2,
                    TipoDespesa = TipoDespesa.Alimentacao,
                    Descricao = TipoDespesa.Alimentacao.GetDescription(),
                    DataCriacao = DateTime.Now,
                    ValorPrevisto = 1000.00m,
                },

                new Despesa
                {
                    Id = 8,
                    OrcamentoId = 2,
                    TipoDespesa = TipoDespesa.Condominio,
                    Descricao = TipoDespesa.Condominio.GetDescription(),
                    DataCriacao = DateTime.Now,
                    ValorPrevisto = 450.00m,
                },

            });
        }
    }
}
