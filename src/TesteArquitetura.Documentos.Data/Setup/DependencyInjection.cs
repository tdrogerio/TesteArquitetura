using TesteArquitetura.Documentos.Data.Repository;
using TesteArquitetura.Documentos.Domain.Repositories;
using TesteArquitetura.Documentos.Domain.Repositories.Views;
using Microsoft.Extensions.DependencyInjection;

namespace TesteArquitetura.Documentos.Data.Setup
{
    public static class DependencyInjection
    {
        public static void AddDataLayer(this IServiceCollection services)
        {
            #region [Infra]
            services.AddScoped<ILogRepository, LogRepository>();
            #endregion

            #region [FluxoCaixa]
            services.AddScoped<IFluxoCaixaRepository, FluxoCaixaRepository>();
            #endregion

            #region [Context]
            services.AddScoped<DocumentosContext>();
            #endregion

        }
    }
}
