using TesteArquitetura.Documentos.Application.Services;
using TesteArquitetura.Documentos.Application.Services.Interfaces;
using TesteArquitetura.Documentos.Application.Services.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TesteArquitetura.Documentos.Application.Setup
{
    public static class DependencyInjection
    {
        public static void AddAplicationLayer(this IServiceCollection services)
        {
            #region [FluxoCaixa]
            services.AddScoped<IFluxoCaixaAppService, FluxoCaixaAppService>();
            #endregion


            services.AddScoped<ILogger, TesteArquitetura.Core.Log.Elmah>();
        }
    }
}
