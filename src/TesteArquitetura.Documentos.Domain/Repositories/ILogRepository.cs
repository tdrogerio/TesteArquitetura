namespace TesteArquitetura.Documentos.Domain.Repositories
{
    public interface ILogRepository : IRepository<Log>
    {
        Task<Log> SaveLog(Log log);
    }
}
