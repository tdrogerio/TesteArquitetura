using Duende.IdentityServer.EntityFramework.Options;
using TesteArquitetura.WebApi.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace TesteArquitetura.WebApi.Data
{
    public class ApiApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApiApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {

        }
    }
}
