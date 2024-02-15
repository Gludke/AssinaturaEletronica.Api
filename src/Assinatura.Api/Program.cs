using Assinatura.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

// ########## Add Services ##########

builder.Services.AddApiConfig();//configs gerais isoladas

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSwaggerGen();

builder.Services.ResolveIoC();

var app = builder.Build();



// ########## Configure Services ##########

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseApiConfig(app.Environment);

app.MapControllers();

app.Run();
