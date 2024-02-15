using Assinatura.Infra.Drive.UrCompanyApi;

namespace Assinatura.Api.Configuration;

public static class IoCConfig
{
    public static IServiceCollection ResolveIoC(this IServiceCollection service)
    {
        //service.AddScoped<MeuDbContext>();

        //'AddSingleton' - mesma instância para todos os users logados. O .Net, nesse caso, não confunde os contextos.
        //service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        service.AddScoped<IUrCompanyApi, UrCompanyApi>();

        return service;
    }
}
