using Elmah.Io.AspNetCore;
using Microsoft.Extensions.Options;
using Elmah.Io.Extensions.Logging;

namespace TesteArquitetura.WebApi.Configuration
{
    public static class ElmahConfig
    {
        public static IServiceCollection AddElmahConfig(this IServiceCollection services, WebApplicationBuilder builder)
        {
            builder.Logging.Services.Configure<ElmahIoProviderOptions>(builder.Configuration.GetSection("ElmahIo"));
            builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
            builder.Logging.SetMinimumLevel(LogLevel.Error);
            builder.Logging.SetMinimumLevel(LogLevel.Critical);

            builder.Logging.ClearProviders();


            builder.Logging.AddElmahIo();

            return services;
        }
    }

    public class DecorateElmahIoMessages : IConfigureOptions<ElmahIoOptions>
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public DecorateElmahIoMessages(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public void Configure(ElmahIoOptions options)
        {
            options.OnMessage = msg =>
            {
                var context = httpContextAccessor.HttpContext;
                msg.User = context?.User?.Identity?.Name;
            };
        }
    }
}
