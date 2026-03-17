using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estac.Infra.EntityBuilders
{
    public class TransportadoraMapping : IEntityTypeConfiguration<Transportadora>
    {
        public void Configure(EntityTypeBuilder<Transportadora> builder)
        {
            builder.ToTable("Transportadora", "gts");
            builder.HasKey(m => m.Id);

            builder.Property(v => v.Descricao)
               .HasColumnName("Descricao")
               .HasColumnType("varchar(150)")
               .HasMaxLength(150);

            builder.Property(m => m.PessoaId)
                   .IsRequired();

            builder.HasOne(m => m.Pessoa)
                   .WithMany() // ou .WithMany(p => p.Motoristas) se existir coleção
                   .HasForeignKey(m => m.PessoaId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
