using Estac.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estac.Infra.EntityBuilders
{
    public class MotoristaMapping : IEntityTypeConfiguration<Motorista>
    {
        public void Configure(EntityTypeBuilder<Motorista> builder)
        {
            builder.ToTable("Motorista");

            builder.HasKey(m => m.Id);

            builder.Property(v => v.Descricao)
               .HasColumnName("Descricao")
               .HasColumnType("varchar(150)")
               .HasMaxLength(150);

            builder.Property(m => m.PessoaId)
                   .IsRequired();

            builder.Property(m => m.CNH)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(m => m.ValidadeCNH)
                   .IsRequired(false);

            builder.HasOne(m => m.Pessoa)
                   .WithMany() // ou .WithMany(p => p.Motoristas) se existir coleção
                   .HasForeignKey(m => m.PessoaId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
