using TesteArquitetura.Documentos.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TesteArquitetura.Documentos.Data.Mapping
{
    public class FluxoCaixaMapping : IEntityTypeConfiguration<FluxoCaixa>
    {
        public void Configure(EntityTypeBuilder<FluxoCaixa> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Codigo).IsRequired().HasColumnType("char(15)");
            builder.Property(x => x.IsAtivo).HasColumnType("bit").HasDefaultValue(true);
            builder.Property(x => x.DataCadastro).HasColumnType("datetime");
            builder.Property(x => x.DataAlteracao).HasColumnType("datetime");

            builder.ToTable("FluxoCaixa", "FluxoCaixa");
        }
    }
}
