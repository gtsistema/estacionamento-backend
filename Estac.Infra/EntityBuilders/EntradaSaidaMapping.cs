using Estac.Domain.Models;
using Estac.Domain.Models.Enuns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estac.Infra.EntityBuilders
{
    public class EntradaSaidaMapping : IEntityTypeConfiguration<EntradaSaida>
    {
        public void Configure(EntityTypeBuilder<EntradaSaida> builder)
        {
            builder.ToTable("EntradaSaida", "gts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Descricao)
                .HasColumnType("varchar(150)")
                .HasMaxLength(150);

            builder.Property(x => x.MotoristaId)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.TransportadoraId)
                .HasColumnType("int")
                .IsRequired(false);

            builder.Property(x => x.VeiculoId)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.DataHoraEntrada)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(x => x.DataHoraSaida)
                .HasColumnType("datetime")
                .IsRequired(false);

            builder.Property(x => x.UsuarioRegistroEntradaId)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.UsuarioRegistroEntradaNome)
                .HasColumnType("varchar(150)")
                .HasMaxLength(150)
                .IsRequired(false);

            builder.Property(x => x.UsuarioFinalizacaoId)
                .HasColumnType("int")
                .IsRequired(false);

            builder.Property(x => x.UsuarioFinalizacaoNome)
                .HasColumnType("varchar(150)")
                .HasMaxLength(150)
                .IsRequired(false);

            builder.Property(x => x.DataHoraUltimaEntradaPatio)
                .HasColumnType("datetime")
                .IsRequired(false);

            builder.Property(x => x.TempoPermanenciaMinutos)
                .HasColumnType("int")
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(x => x.TempoTotalSuspensaoMinutos)
                .HasColumnType("int")
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(x => x.PermanenciaSuspensa)
                .HasColumnType("bit")
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(x => x.Finalizado)
                .HasColumnType("bit")
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(x => x.Status)
                .HasConversion<byte>()
                .HasColumnType("tinyint")
                .IsRequired()
                .HasDefaultValue(EntradaSaidaStatus.Entrada);

            builder.Property(x => x.DataHoraFinalizacao)
                .HasColumnType("datetime")
                .IsRequired(false);

            builder.HasOne(x => x.Motorista)
                .WithMany()
                .HasForeignKey(x => x.MotoristaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Transportadora)
                .WithMany()
                .HasForeignKey(x => x.TransportadoraId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Veiculo)
                .WithMany()
                .HasForeignKey(x => x.VeiculoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
