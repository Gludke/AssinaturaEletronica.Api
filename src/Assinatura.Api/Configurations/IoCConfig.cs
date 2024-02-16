using Assinatura.Domain.Services;
using Assinatura.Domain.Services.Interfaces;
using Assinatura.Infra.Drive.UrCompanyApi;

namespace Assinatura.Api.Configuration;

public static class IoCConfig
{
    public static IServiceCollection ResolveIoC(this IServiceCollection service)
    {
        //service.AddScoped<SqlDbContext>();

        service.AddScoped<IUrCompanyApi, UrCompanyApi>();
        service.AddScoped<IDocService, DocService>();

        return service;
    }
}
