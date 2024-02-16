using Assinatura.Domain.Services;
using Assinatura.Domain.Services.Interfaces;
using Assinatura.Infra.Data;
using Assinatura.Infra.Data.Interfaces;
using Assinatura.Infra.Drive.UrCompanyApi;

namespace Assinatura.Api.Configuration;

public static class IoCConfig
{
    public static IServiceCollection ResolveIoC(this IServiceCollection service)
    {
        //service.AddScoped<SqlDbContext>();

        service.AddScoped<IUrCompanyApi, UrCompanyApi>();
        service.AddScoped<IDocService, DocService>();
        service.AddScoped<ITokenCacheService, TokenCacheService>();

        return service;
    }
}
