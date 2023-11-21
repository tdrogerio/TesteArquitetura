using TesteArquitetura.Documentos.Domain;
using TesteArquitetura.Documentos.Domain.Repositories;

namespace TesteArquitetura.Documentos.Data.Repository
{
    public class FluxoCaixaRepository : Repository<FluxoCaixa>, IFluxoCaixaRepository
    {
        public FluxoCaixaRepository(DocumentosContext context) : base(context)
        {
        }
    }
}
