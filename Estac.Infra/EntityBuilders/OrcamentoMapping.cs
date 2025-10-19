using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estac.Infra.EntityBuilders
{
    public class OrcamentoMapping : IEntityTypeConfiguration<Orcamento>
    {
        public void Configure(EntityTypeBuilder<Orcamento> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .UseIdentityColumn(1, 1);


            builder.Property(x => x.Descricao)
             .HasMaxLength(200);

            builder.Property(x => x.ValorTotalPagoDespesas);

            builder.Property(x => x.ValorTotalPagoReceitas);

            // Relacionamento entre Orcamento e Despesa (OneToMany)
            builder.HasMany(o => o.Despesas) // Um Orcamento pode ter várias Despesas
                   .WithOne(d => d.Orcamento) // Uma Despesa pertence a um único Orcamento
                   .HasForeignKey(d => d.OrcamentoId); // Chave estrangeira em Despesa

            // Relacionamento entre Orcamento e Receita (OneToMany)
            builder.HasMany(o => o.Receitas) // Um Orcamento pode ter várias Receitas
                   .WithOne(r => r.Orcamento) // Uma Receita pertence a um único Orcamento
                   .HasForeignKey(r => r.OrcamentoId); // Chave estrangeira em Receita


            // ===================SEED====
            builder.HasData(new List<Orcamento>
            {
                new Orcamento
                {
                    Id = 1,
                    Ano = 2025,
                    Descricao = "Despesas Mensais - 2025",
                    DataCriacao = DateTime.Now,
                    ValorTotalPagoDespesas = 0,
                    ValorTotalPagoReceitas = 0,
                    DataLimite = new DateTime(2025, 12, 31),
                },

                new Orcamento
                {
                    Id = 2,
                    Ano = 2025,
                    Descricao = "Volta para Cuiabá",
                    DataCriacao = DateTime.Now,
                    ValorTotalPagoDespesas = 0,
                    ValorTotalPagoReceitas = 0,
                    DataLimite = new DateTime(2025, 12, 31)
                },

                new Orcamento
                {
                    Id = 3,
                    Descricao = "Compra da Moto",
                    DataCriacao = DateTime.Now,
                    ValorTotalPagoDespesas = 0,
                    ValorTotalPagoReceitas = 0,
                    NaoPossuiDataLimite = true
                },

                new Orcamento
                {
                    Id = 4,
                    Descricao = "Fundo de Reserva",
                    DataCriacao = DateTime.Now,
                    ValorTotalPagoDespesas = 0,
                    ValorTotalPagoReceitas = 0,
                    NaoPossuiDataLimite = true
                },
            });
        }
    }
}
