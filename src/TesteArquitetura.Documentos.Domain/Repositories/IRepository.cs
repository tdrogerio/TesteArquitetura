using Microsoft.EntityFrameworkCore.Storage;

namespace TesteArquitetura.Documentos.Domain.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        IQueryable<T> AsQueryable();
        Task<T> GetByIdAsync(Guid id);
        Task<bool> Exists(Guid id);
        Task<T> SaveAsync(T entity);
        Task<T> UpdateAsync(T entity);
        void Delete(T entity);
        Task Commit();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync();
    }
}
