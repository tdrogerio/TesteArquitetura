namespace TesteArquitetura.Documentos.Application.Services.Interfaces
{
    public interface IVWService<TVwEntity, TVwViewModel>
    {
        IEnumerable<TVwViewModel> GetViewResult(string viewName);
    }
}
