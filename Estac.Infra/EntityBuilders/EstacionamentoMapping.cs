using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estac.Infra.EntityBuilders
{
    public class EstacionamentoMapping : IEntityTypeConfiguration<Estacionamento>
    {
        public void Configure(EntityTypeBuilder<Estacionamento> builder)
        {
            builder.ToTable("Estacionamento", "gts");

            builder.HasKey(m => m.Id);

            builder.Property(v => v.Descricao)
               .HasColumnName("Descricao")
               .HasColumnType("varchar(150)")
               .HasMaxLength(150);

            builder.Property(v => v.ResposanvelLegal)
               .HasColumnName("ResposanvelLegal")
               .HasColumnType("varchar(150)")
               .HasMaxLength(150);

            builder.Property(v => v.ResponsavelCpf)
              .HasColumnName("ResponsavelCpf")
              .HasColumnType("varchar(14)")
              .HasMaxLength(14);

            builder.Property(v => v.TamanhoTerreno)
               .HasColumnType("varchar(15)")
               .HasMaxLength(15);

            builder.Property(v => v.TipoCobranca)
               .HasConversion<byte>()
               .HasColumnType("tinyint")
               .HasDefaultValue(TipoCobranca.Gratuito)
               .IsRequired();

            builder.Property(v => v.CobrancaValor)
              .HasColumnType("decimal(18,2)");

            builder.Property(m => m.PessoaId)
                   .IsRequired();

            builder.HasOne(m => m.Pessoa)
                   .WithMany()
                   .HasForeignKey(m => m.PessoaId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
