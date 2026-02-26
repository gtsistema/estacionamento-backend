using Estac.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estac.Infra.EntityBuilders
{
    public class EstacionamentoMapping : IEntityTypeConfiguration<Estacionamento>
    {
        public void Configure(EntityTypeBuilder<Estacionamento> builder)
        {
            builder.ToTable("Estacionamento");

            builder.HasKey(m => m.Id);

            builder.Property(v => v.Descricao)
               .HasColumnName("Descricao")
               .HasColumnType("varchar(150)")
               .HasMaxLength(150);

            builder.Property(m => m.PessoaId)
                   .IsRequired();

            builder.HasOne(m => m.Pessoa)
                   .WithMany() // ou .WithMany(p => p.Estacionamentos) se existir coleção
                   .HasForeignKey(m => m.PessoaId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
