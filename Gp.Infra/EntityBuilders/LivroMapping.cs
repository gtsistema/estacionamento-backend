using Gp.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gp.Infra.EntityBuilders
{
    public class LivroMapping : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable("Livro");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Valor)
            .HasPrecision(10, 4);
        }
    }
}