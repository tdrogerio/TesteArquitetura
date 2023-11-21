using TesteArquitetura.Documentos.Application.AutoMapper;
using TesteArquitetura.Documentos.Data;
using TesteArquitetura.Documentos.Data.Triggers;
using TesteArquitetura.Documentos.Domain;
using TesteArquitetura.WebApi.Configuration;
using TesteArquitetura.WebApi.Setup;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Elmah.Io.AspNetCore;
using Elmah.Io.Extensions.Logging;
using Microsoft.ApplicationInsights.DependencyCollector;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DocumentosContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.UseTriggers(triggerOptions =>
    {
        triggerOptions.AddTrigger<BeforeSaveTrigger>();
    });
});

//Configura JWT - setando roles, polices customizadas, pode também configurar tokens,
//autenticação de dois fatores, assim como também autenticação por e-mail ou telefonefone
builder.Services.AddIdentityConfiguration(builder);

builder.Services.AddNotificationConfig(builder);

builder.Services.AddElmahConfig(builder);

// Dependency Injection
builder.Services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));

//Configurações da API - nome, número da versão
builder.Services.AddApiConfig();

builder.Services.AddEndpointsApiExplorer();

//Configurações do Swagger, onde solicita o token Bearer no formato JWT
builder.Services.AddSwaggerConfig();

builder.Services.RegisterServices();

builder.Services.AddAntiforgery(options =>
{
    // Set Cookie properties using CookieBuilder properties.
    options.FormFieldName = "AntiforgeryFieldname";
    options.HeaderName = "X-CSRF-TOKEN-HEADERNAME";
    options.SuppressXFrameOptionsHeader = false;
});

builder.Services.AddApplicationInsightsTelemetry();

builder.Services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>(
    (module, o) =>
    {
        module.EnableSqlCommandTextInstrumentation = true;
    });


builder.Services.AddSingleton<HtmlEncoder>(
     HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin,
                                               UnicodeRanges.CjkUnifiedIdeographs }));

WebApplication? app = builder.Build();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseApiConfig(app.Environment);

app.UseSwaggerConfig(apiVersionDescriptionProvider);

var antiforgery = app.Services.GetRequiredService<IAntiforgery>();

app.Use((context, next) =>
{
    var requestPath = context.Request.Path.Value;

    if (string.Equals(requestPath, "/", StringComparison.OrdinalIgnoreCase)
        || string.Equals(requestPath, "/index.html", StringComparison.OrdinalIgnoreCase))
    {
        var tokenSet = antiforgery.GetAndStoreTokens(context);
        context.Response.Cookies.Append("XSRF-TOKEN", tokenSet.RequestToken!,
            new CookieOptions { HttpOnly = false });
    }

    return next(context);
});

app.Run();
