using Estac.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estac.Infra.EntityBuilders
{
    public class ConfiguracaoAgendamentoMapping : IEntityTypeConfiguration<ConfiguracaoAgendamento>
    {
        public void Configure(EntityTypeBuilder<ConfiguracaoAgendamento> builder)
        {
            builder.ToTable("ConfiguracaoAgendamento", "gts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.ConfiguracaoCobrancaId)
                   .IsRequired();

            builder.Property(x => x.TipoJob)
                   .HasConversion<int>()
                   .IsRequired();

            builder.Property(x => x.ModalidadeCobranca)
                   .HasConversion<int>()
                   .IsRequired();

            builder.Property(x => x.Intervalo)
                   .HasDefaultValue(1)
                   .IsRequired();

            builder.Property(x => x.DiaSemana)
                   .HasConversion<int?>()
                   .IsRequired(false);

            builder.Property(x => x.DiaMes)
                   .IsRequired(false);

            builder.Property(x => x.HoraExecucao)
                   .HasColumnType("time")
                   .IsRequired();

            builder.Property(x => x.UltimaExecucao)
                   .IsRequired(false);

            builder.Property(x => x.ProximaExecucao)
                   .IsRequired(false);

            builder.Property(x => x.Ativo)
                   .HasDefaultValue(true)
                   .IsRequired();

            builder.Property(x => x.DataCadastro)
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();

            builder.Property(x => x.DataAtualizacao)
                   .IsRequired(false);

            builder.HasOne(x => x.ConfiguracaoCobranca)
                   .WithOne(x => x.ConfiguracaoAgendamento)
                   .HasForeignKey<ConfiguracaoAgendamento>(x => x.ConfiguracaoCobrancaId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.ConfiguracaoCobrancaId)
                   .IsUnique()
                   .HasDatabaseName("IX_ConfiguracaoAgendamento_ConfiguracaoCobrancaId");

            builder.HasIndex(x => x.TipoJob)
                   .HasDatabaseName("IX_ConfiguracaoAgendamento_TipoJob");

            builder.HasIndex(x => x.Ativo)
                   .HasDatabaseName("IX_ConfiguracaoAgendamento_Ativo");
        }
    }
}
