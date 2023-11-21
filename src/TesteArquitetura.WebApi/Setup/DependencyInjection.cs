using TesteArquitetura.Core.Notificacoes;
using TesteArquitetura.Core.Notificacoes.Interfaces;
using TesteArquitetura.Documentos.Application.Setup;
using TesteArquitetura.Documentos.Data.Setup;
using TesteArquitetura.WebApi.Configuration;
using TesteArquitetura.WebApi.Extensions;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TesteArquitetura.WebApi.Setup
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            // Catalogo
            services.AddDataLayer();
            services.AddAplicationLayer();

            // Authentication
            services.AddScoped<INotificador, Notificador>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            // Swagger
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}
