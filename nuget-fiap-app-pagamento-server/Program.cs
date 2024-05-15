using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using Microsoft.Extensions.Caching.Memory;
using nuget_fiap_app_pagamento_server.Interface.Services;
using nuget_fiap_app_pagamento_common.Interfaces;
using nuget_fiap_app_pagamento_common.Interfaces.UseCases;

public partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Adicione configuração do MemoryCache
        builder.Services.AddMemoryCache();
        var configuration = builder.Configuration;

        // Configurando ProdutoAPIRepository com HttpClient e parâmetros necessários
        builder.Services.AddHttpClient("ProdutoAPI", client =>
        {
            client.BaseAddress = new Uri(configuration["ProdutoApi:BaseUrl"]);
        });

        // Registra o ProdutoAPIRepository com os parâmetros requeridos incluindo IMemoryCache e baseUrl

        // Registro de outros serviços e repositórios
        builder.Services.AddScoped<IPagamentoUseCase, PagamentoUseCase>();
        builder.Services.AddMemoryCache();

        // Configuração do HealthCheck e Swagger
        builder.Services.AddHealthChecks();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "NuGET Burger",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Name = "Miro",
                    Url = new Uri("https://miro.com/app/board/uXjVMqYSzbg=/?share_link_id=124875092732")
                }
            });
        });

        var app = builder.Build();

        // Configuração do pipeline de requisições HTTP
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "NuGET Burger API V1");
        });
        app.UseReDoc(c =>
        {
            c.DocumentTitle = "REDOC API Documentation";
            c.SpecUrl("/swagger/v1/swagger.json");
        });

        app.UseAuthorization();
        app.MapControllers();
        app.MapHealthChecks("/health", new HealthCheckOptions()
        {
            ResultStatusCodes =
            {
                [HealthStatus.Healthy] = StatusCodes.Status200OK,
                [HealthStatus.Degraded] = StatusCodes.Status200OK,
                [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
            },
        });

        app.Run();
    }
}
