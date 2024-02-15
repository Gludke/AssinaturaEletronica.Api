using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Assinatura.Api.Configuration;

public static class ApiConfig
{
    public static IServiceCollection AddApiConfig(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        //Impede que a API faça as suas validações automáticas
        services.Configure<ApiBehaviorOptions>(opt =>
        {
            opt.SuppressModelStateInvalidFilter = true;
        });

        //Políticas de acesso
        services.AddCors(options =>
        {
            //Políticas de nome 'Development': acesso total atribuído nessas políticas
            options.AddPolicy("Development",
                builder =>
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

        return services;
    }

    public static IApplicationBuilder UseApiConfig(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseHttpsRedirection();

        //Usando a política de acesso 'Development' configurada acima
        app.UseCors("Development");

        app.UseAuthorization();

        return app;
    }

}
