using Estac.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estac.Infra.EntityBuilders
{
    public class EntradaSaidaSuspensaoMapping : IEntityTypeConfiguration<EntradaSaidaSuspensao>
    {
        public void Configure(EntityTypeBuilder<EntradaSaidaSuspensao> builder)
        {
            builder.ToTable("EntradaSaidaSuspensao", "gts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EntradaSaidaId)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.DataHoraInicioSuspensao)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(x => x.DataHoraFimSuspensao)
                .HasColumnType("datetime")
                .IsRequired(false);

            builder.Property(x => x.TempoSuspensaoMinutos)
                .HasColumnType("int")
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(x => x.UsuarioSuspensaoId)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.UsuarioSuspensaoNome)
                .HasColumnType("varchar(150)")
                .HasMaxLength(150)
                .IsRequired(false);

            builder.HasOne(x => x.EntradaSaida)
                .WithMany(x => x.Suspensoes)
                .HasForeignKey(x => x.EntradaSaidaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
