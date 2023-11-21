using TesteArquitetura.Documentos.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TesteArquitetura.Documentos.Data.Mapping
{
    internal class LogMapping : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IdDominio).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Dominio).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Acao).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Message).IsRequired().HasMaxLength(100);
            builder.Property(x => x.JsonObjectException).IsRequired().HasColumnType("nvarchar(max)");
            builder.Property(x => x.IsAtivo).HasColumnType("bit").HasDefaultValue(true);
            builder.Property(x => x.DataCadastro).HasColumnType("datetime");
            builder.Property(x => x.DataAlteracao).HasColumnType("datetime");
            builder.ToTable("Log", "Infra");
        }
    }
}
