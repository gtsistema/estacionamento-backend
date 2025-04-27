using Gp.Domain.Models;
using Gp.Domain.Models.Enuns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gp.Infra.EntityBuilders
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

            builder.Property(x => x.ValorTotalDespesas);

            builder.Property(x => x.ValorTotalReceitas);

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
                    Ano = 2026,
                    Descricao = "Viagem Cuiabá Férias",
                    DataCriacao = DateTime.Now,
                    ValorTotalDespesas = 0,
                    ValorTotalReceitas = 0,
                    MesDoAno = MesDoAno.Janeiro,
                },

                new Orcamento
                {
                    Id = 2,
                    Ano = 2025,
                    Descricao = "Despesa Mensal",
                    DataCriacao = DateTime.Now,
                    ValorTotalDespesas = 0,
                    ValorTotalReceitas = 0,
                    MesDoAno = MesDoAno.Maio,
                },

                new Orcamento
                {
                    Id = 3,
                    Ano = 2025,
                    Descricao = "Investimentos",
                    DataCriacao = DateTime.Now,
                    ValorTotalDespesas = 0,
                    ValorTotalReceitas = 0,
                    MesDoAno = MesDoAno.SemPrazo,
                },

                 new Orcamento
                {
                    Id = 4,
                    Ano = 2025,
                    Descricao = "Temporário",
                    DataCriacao = DateTime.Now,
                    ValorTotalDespesas = 0,
                    ValorTotalReceitas = 0,
                    MesDoAno = MesDoAno.SemPrazo,
                },
            });
        }
    }
}
