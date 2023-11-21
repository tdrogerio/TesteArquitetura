namespace TesteArquitetura.Documentos.Application.Services.Interfaces
{
    public interface IService<TEntity, TViewModel>
    {
        Task<IEnumerable<TViewModel>> GetAll();
        Task<TViewModel> GetByIdAsync(Guid id);
        Task<TViewModel> SaveAsync(TViewModel obj);
        Task<TViewModel> UpdateAsync(TViewModel obj);
        Task RemoveAsync(TViewModel obj);
    }
}
