using TesteArquitetura.Documentos.Domain;
using TesteArquitetura.Documentos.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TesteArquitetura.Documentos.Data.Repository
{
    public class LogRepository : Repository<Log>, ILogRepository
    {
        private new readonly DocumentosContext _context;
        protected new readonly DbSet<Log> _dbSet;
        public LogRepository(DocumentosContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Log>();
        }

        public async Task<Log> SaveLog(Log log)
        {
            var entityEntry = await _dbSet.AddAsync(log);
            _context.SaveChanges();

            return entityEntry.Entity;
        }
    }
}
