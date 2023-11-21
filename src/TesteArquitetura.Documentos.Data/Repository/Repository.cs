using TesteArquitetura.Documentos.Domain;
using TesteArquitetura.Documentos.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace TesteArquitetura.Documentos.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly DocumentosContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(DocumentosContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> AsQueryable() => _dbSet;

        public async Task<TEntity> GetByIdAsync(Guid id) =>
            await _dbSet.FindAsync(new object[] { id });

        public async Task<bool> Exists(Guid id) =>
            await _dbSet.AsNoTracking().AnyAsync(x => Equals(x.Id, id));

        public async Task<TEntity> SaveAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentException("Não pode adicionar entidade nula");

            var entityEntry = await _dbSet.AddAsync(entity);
            _context.SaveChanges();

            return entityEntry.Entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var id = await Exists(entity.Id);
            if (!id)
                throw new ArgumentException("Não pode adicionar entidade nula");

            var entityEntry = _dbSet.Update(entity);
            _context.SaveChanges();

            return entityEntry.Entity;
        }

        public void Delete(TEntity entity)
        {
            try
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        } 

        public async Task Commit() => await _context.SaveChangesAsync(true);

        public async Task<IDbContextTransaction> BeginTransactionAsync() => await _context.Database.BeginTransactionAsync();

        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

    }
}
