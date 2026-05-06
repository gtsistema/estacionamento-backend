using Estac.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estac.Infra.EntityBuilders
{
    public class PessoaContatoMapping : IEntityTypeConfiguration<PessoaContato>
    {
        public void Configure(EntityTypeBuilder<PessoaContato> builder)
        {
            builder.ToTable("PessoaContato", "gts");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Descricao)
                .HasColumnType("varchar(200)")
                .HasMaxLength(200);

            builder.Property(p => p.PessoaId)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.Cpf)
                .HasColumnType("varchar(14)")
                .HasMaxLength(14);

            builder.Property(p => p.Telefone)
                .HasColumnType("varchar(20)")
                .HasMaxLength(20);

            builder.Property(p => p.Email)
                .HasColumnType("varchar(150)")
                .HasMaxLength(150);

            builder.Property(p => p.Principal)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(p => p.Observacao)
                .HasColumnType("varchar(500)")
                .HasMaxLength(500);

            builder.HasOne(p => p.Pessoa)
                .WithMany(p => p.Contatos)
                .HasForeignKey(p => p.PessoaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.PessoaId);
        }
    }
}
