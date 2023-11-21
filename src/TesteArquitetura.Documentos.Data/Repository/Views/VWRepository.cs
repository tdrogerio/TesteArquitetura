using TesteArquitetura.Documentos.Domain;
using TesteArquitetura.Documentos.Domain.Repositories;
using TesteArquitetura.Documentos.Domain.Repositories.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace TesteArquitetura.Documentos.Data.Repository
{
    public abstract class VWRepository<TEntity> : IVWRepository<TEntity> where TEntity : Entity
    {
        protected readonly DocumentosContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public VWRepository(DocumentosContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        /// <summary>
        /// Pode trabalhar com procedures da mesma forma.
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetViewResult(string viewName)
        {
            return _dbSet.FromSqlRaw(String.Format(@"SELECT * FROM {0}", viewName));
        }
    }
}
