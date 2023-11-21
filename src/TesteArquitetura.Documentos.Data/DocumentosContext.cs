using TesteArquitetura.Documentos.Domain;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace TesteArquitetura.Documentos.Data
{
    public class DocumentosContext : DbContext
    {
        public DocumentosContext(DbContextOptions<DocumentosContext> options)
            : base(options) { }

        public DbSet<FluxoCaixa> FluxoCaixa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DocumentosContext).Assembly);
        }
    }
}
