using Microsoft.EntityFrameworkCore.Storage;

namespace TesteArquitetura.Documentos.Domain.Repositories.Views
{
    public interface IVWRepository<T> where T : Entity
    {
        IEnumerable<T> GetViewResult(string viewName);
    }
}
