using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using TesteArquitetura.Documentos.Domain.Configuration;

namespace TesteArquitetura.WebApi.Configuration
{
    public static class NotificationConfig
    {
        public static IServiceCollection AddNotificationConfig(this IServiceCollection services, WebApplicationBuilder builder)
        {
            //Armazenamento seguro no ambiente de desenvolvimento
            SMTP SMTP = builder.Configuration.GetSection("SMTP").Get<SMTP>();
            EmailReenvioDeSenha emailReenvioDeSenha = builder.Configuration.GetSection("EmailReenvioDeSenha").Get<EmailReenvioDeSenha>();

            return services;
        }

    }
}
