using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estac.Infra.EntityBuilders
{
    public class EstacionamentoFotoMapping
    : IEntityTypeConfiguration<EstacionamentoFoto>
    {
        public void Configure(EntityTypeBuilder<EstacionamentoFoto> builder)
        {
            builder.ToTable("EstacionamentoFoto");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedNever();

            builder.Property(x => x.EstacionamentoId)
                   .IsRequired();

            builder.Property(x => x.Foto)
                   .HasColumnType("varbinary(max)")
                   .IsRequired();

            builder.Property(x => x.Descricao)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.ContentType)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Principal)
                   .HasDefaultValue(false);

            builder.Property(x => x.DataCriacao)
                   .HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.Estacionamento)
                   .WithMany(x => x.Fotos)
                   .HasForeignKey(x => x.EstacionamentoId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.EstacionamentoId);

            builder.HasIndex(x => new { x.EstacionamentoId, x.Principal })
                   .HasFilter("[EhPrincipal] = 1")
                   .IsUnique();
        }
    }
}
