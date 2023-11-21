using TesteArquitetura.Documentos.Domain.Configuration;
using TesteArquitetura.WebApi.Data;
using TesteArquitetura.WebApi.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TesteArquitetura.WebApi.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetSection("Connection").Get<Connection>().DefaultConnection));


            services.AddDefaultIdentity<IdentityUser>()
                
                // Trabalha com roles onde vc pode customizar politicas de roles customizados.
                // Nosso objetivo aqui é usar o padrão do Identity.
                .AddRoles<IdentityRole>() 
                .AddEntityFrameworkStores<ApplicationDbContext>()

                // Tratamento das traduções das mensagens de erros para português.
                .AddErrorDescriber<IdentityMensagensPortugues>()

                // Adiciona o recurso para geração de Tokens para resetar senha, validar e-mail do
                // usuário recém cadastrados, etc.
                .AddDefaultTokenProviders();

            // JWT
            var appSettingsSection = builder.Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            //Autenticação do ASP.NET Core
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //Bearer authentication(também conhecido como token authentication) é um Schema para autenticação HTTP(RC6750). O Bearer identifica recursos protegidos por um OAuth2. O<token> deve ser um string.Ele representa uma autorização do Server emitida para o client.
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = appSettings.ValidoEm,
                    ValidIssuer = appSettings.Emissor
                };
            });

            return services;
        }
    }
}
